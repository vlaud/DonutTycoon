using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickManager : MonoBehaviour
{
    [SerializeField] private float doubleClickThreshold = 0.25f;

    private float lastClickTime = 0f;
    [SerializeField] private ClickController lastClickedTarget = null;
    private bool isWaitingForDoubleClick = false;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D hit = Physics2D.OverlapPoint(mousePos);

            ClickController clickedTarget = hit != null ? hit.GetComponent<ClickController>() : null;

            if (clickedTarget == null || clickedTarget != lastClickedTarget)
            {
                // Notify the last clicked target that the click was missed
                NotifyClickMissed();
            }

            if (clickedTarget != null)
            {
                HandleClick(clickedTarget);
            }
        }
    }

    private void HandleClick(ClickController target)
    {
        float time = Time.time;

        if (isWaitingForDoubleClick
            && target == lastClickedTarget
            && time - lastClickTime <= doubleClickThreshold)
        {
            // 더블 클릭
            isWaitingForDoubleClick = false;
            target.onDoubleClick.Invoke();
        }
        else
        {
            // 싱글 클릭
            lastClickTime = time;
            lastClickedTarget = target;
            isWaitingForDoubleClick = true;
            StartCoroutine(WaitForDoubleClick(target));
        }
    }

    private IEnumerator WaitForDoubleClick(ClickController target)
    {
        yield return new WaitForSeconds(doubleClickThreshold);

        if (isWaitingForDoubleClick && target == lastClickedTarget)
        {
            target.onSingleClick.Invoke();
            isWaitingForDoubleClick = false;
        }
    }

    private void NotifyClickMissed()
    {
        // Notify the ClickController that the click was missed
        if (lastClickedTarget != null)
        {
            lastClickedTarget.onClickMissed.Invoke();
            lastClickedTarget = null;
        }
    }
}