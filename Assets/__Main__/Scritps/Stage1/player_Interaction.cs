
using UnityEngine;

namespace CH1
{
    public class player_Interaction : MonoBehaviour
    {
        public DialogueSO dialoguesData;
        public GameObject answerPanelObject;



        private void OnMouseDown()
        {
            if (!Stage1Manager.Instance.canClick) return;

            Stage1Manager.Instance.canClick = false;
            Stage1Manager.Instance.activeCharacterObject = gameObject;
            // TODO : Zoom Camera to character
            Stage1Manager.Instance.ZoomIn(transform);

            // Show Dialogues
            DialogueEvents.StartDialogues(dialoguesData);

            // Add Listerner to dialogue end
            DialogueEvents.OnDialoguesEnd += DialogueEnd;
        }

        private void DialogueEnd()
        {
            answerPanelObject.SetActive(true);
            Stage1Manager.Instance.canClick = true;
            Destroy(this);
        }
    }
}
