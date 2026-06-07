using UnityEngine;

//프리팹마다 인스펙터에서 텍스트만 채우쇼
public class StoryNote : MonoBehaviour
{
    [TextArea(3, 10)]
    public string storyText;
}