
using UnityEngine;

public class HospitalGameManager : MonoBehaviour
{
    public void checkAnsewr(bool check) {
        if (check == true)
        {
            Debug.Log("Correct Answer");
        }
        else {
            if (check == false)
            {
                Debug.Log("Wrong Answer");
            }
        }
    }
}
