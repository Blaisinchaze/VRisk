using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class MenuController : MonoBehaviour
{
    public enum OpenMode
    {
        SCALE,
        SLIDE
    }

    public OpenMode openMode = OpenMode.SCALE;
    public float animationTime = 0.5f;

    [SerializeField] private Vector3 defaultPos;
    [SerializeField] private Vector3 awayPos;
    
    [SerializeField] private bool startOpen = false;
    [SerializeField] private bool open = true;

    void Start()
    {
        defaultPos = transform.localPosition;
        
        if (!startOpen)
        {
            switch (openMode)
            {
                case OpenMode.SCALE:
                    transform.localScale = new Vector3(0, 0, 1);
                    break;
                
                case OpenMode.SLIDE:
                    transform.localScale = awayPos;
                    break;
            }
            
            open = false;
        }
    }

    public void OpenMenu()
    {
        open = true;

        switch (openMode)
        {
            case OpenMode.SCALE:
                transform.LeanScale(new Vector3(1,1,1), animationTime);
                break;
            
            case OpenMode.SLIDE:
                transform.LeanMoveLocal(defaultPos, animationTime).setEaseInOutBack();
                break;
        }
    }

    public void CloseMenu()
    {
        open = false;
        
        switch (openMode)
        {
            case OpenMode.SCALE:
                transform.LeanScale(new Vector3(0,0,1), animationTime).setEaseInBack();
                break;
            
            case OpenMode.SLIDE:
                transform.LeanMoveLocal(awayPos, animationTime).setEaseInOutBack();
                break;
        }
    }

    public void ToggleOpenClose()
    {
        if (open)
        {
            CloseMenu();
        }
        else
        {
            OpenMenu();
        }
    }
}
