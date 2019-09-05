using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    /*************
     * FUNCTIONS *
     *************/

    /**Checkpoint**/

    // Set the lastcheckpoint variable in the gamemanager to this checkpoint
    public void Check()
    {
        GameManager.Instance.SetLastCheckpoint(this);
    }
}
