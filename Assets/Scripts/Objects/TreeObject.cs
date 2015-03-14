using UnityEngine;
using System.Collections;

public class TreeObject : MonoBehaviour {

    // Public Variables
    public SpriteRenderer Smiley;
    public Sprite SmileyHappy, SmileyNeutral, SmileySad;
    public AnimationClip plantAnimation, growAnimation;

    // Private Variables
    private TreeSettings tree;
    private enum EvaluationResult { Happy, Neutral, Sad };
    private bool evaluated;

	// Initialize
    public void Initialize(TreeSettings tmpTree)
    {
        tree = tmpTree;
        gameObject.renderer.enabled = true;
        Smiley.renderer.enabled = true;
        Smiley.sprite = SmileyNeutral;
        GetComponent<SpriteRenderer>().sprite = tree.Planted;

        PlayAnimation(plantAnimation);
        PlaySound(SoundTypes.PlantTreeSuccess);
    }

    // Remove tree
    public void RemoveTree()
    {
        tree = null;
        evaluated = false;
        gameObject.renderer.enabled = false;
        Smiley.renderer.enabled = false;
        PlaySound(SoundTypes.RemoveTree);
    }

    public TreeSettings GetTree()
    {
        return tree;
    }

    public Sprite GetSmiley()
    {
        return Smiley.GetComponent<SpriteRenderer>().sprite;
    }

    // Evaluate tree and return calculated reward
    public float[] EvaluateTree(string light, bool city, string zone, string humidity, string phvalue)
    {

        int properLA = 0;
        int maxValue = 4;

        if (tree != null)
        {
            // Light
            for (int i = 0; i < tree.light.Length; i++)
            {
                if (tree.light[i] == light)
                {
                    properLA++;
                    break;
                }
            }

            // City
            if (city)
            {
                maxValue++;

                if (tree.cityCompatibility)
                {
                    properLA++;
                }
            }

            // Zone
            for (int i = 0; i < tree.zone.Length; i++) 
            {
                if (tree.zone[i] == zone)
                {
                    properLA++;
                    break;
                }
            }

            // Humidity
            for (int i = 0; i < tree.humidity.Length; i++)
            {
                if (tree.humidity[i] == humidity)
                {
                    properLA++;
                    break;
                }
            }

            // phValue
            for (int i = 0; i < tree.phvalue.Length; i++)
            {
                if (tree.phvalue[i] == phvalue)
                {
                    properLA++;
                    break;
                }
            }
            print("rgerg: " + ((float)properLA / maxValue));
            // Evaluation
            if (properLA / maxValue == 1)
            {
                return EvaluationReward(EvaluationResult.Happy);
            }
            else
            {
                if ((float)properLA / maxValue > 0.5f)
                {
                    return EvaluationReward(EvaluationResult.Neutral);
                }  
            }

            return EvaluationReward(EvaluationResult.Sad);
        }

        return new float[] { 0, 0 };
    }

    private float[] EvaluationReward(EvaluationResult value)
    {
        float tmpEnergy = 0;

        // Only rewarding the player once
        if (!evaluated)
        {
            evaluated = true;
            tmpEnergy = tree.energyReward;
            PlayAnimation(growAnimation);
        }

        switch (value)
        {
            case EvaluationResult.Happy:
                GetComponent<SpriteRenderer>().sprite = tree.FullyGrown;
                Smiley.sprite = SmileyHappy;    
                return new float[] { tree.greenMeter, tmpEnergy };
            case EvaluationResult.Neutral:
                GetComponent<SpriteRenderer>().sprite = tree.FullyGrown;
                Smiley.sprite = SmileyNeutral;
                return new float[] { tree.greenMeter / 2f, tmpEnergy / 2f };
            case EvaluationResult.Sad:
                GetComponent<SpriteRenderer>().sprite = tree.Dead;
                Smiley.sprite = SmileySad;
                return new float[]{ 0, 0};
        }

        return new float[] { 0, 0 };
    }

    private void PlayAnimation(AnimationClip clip)
    {
        if (GameObject.Find(Planet.GetPlanetName()).GetComponent<Planet>().GetEffectIntensity() == AudiovisualEffects.On)
        {
            Animation anim = GetComponent<Animation>();

            anim.clip = clip;
            anim.Play();
        } 
    }

    private void PlaySound(SoundTypes type)
    {
        GameObject tmpPlanet = GameObject.Find(Planet.GetPlanetName());
        if (tmpPlanet.GetComponent<Planet>().GetEffectIntensity() == AudiovisualEffects.On)
        {
            tmpPlanet.GetComponent<AudioController>().PlaySound(type);
        } 
    }
}
