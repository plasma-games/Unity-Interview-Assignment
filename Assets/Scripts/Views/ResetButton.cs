using UnityEngine;
using System.Collections;

// The ResetButton moves the orbs back to their original positions and resets
// the output's colors.
public class ResetButton : MonoBehaviour
{
	[SerializeField] private EnergyOrb[] orbs;
	[SerializeField] private QuestionOutput[] outputs;

	public void ResetOrbs()
	{
		foreach(EnergyOrb orb in orbs)
		{
			orb.ResetOrb();
		}

		foreach (QuestionOutput output in outputs)
		{
			output.SetColor(Color.white);
		}
	}
}

