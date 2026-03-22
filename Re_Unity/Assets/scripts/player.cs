using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class player : MonoBehaviour
{
    public float speed = 5f;
    

    Vector3 moveVec;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 3.현재 연결된 키보드 장치 정보를 가져옴
        var keyboard = Keyboard.current;

        // 키보드가 연결되어 있지 않다면 아래 코드를 실행하지 않고 돌아감 (방어 코드)
        if (keyboard == null) return;

        float moveX = 0;
        float moveZ = 0;

        // 5. 각 키의 상태(isPressed)를 체크하여 방향 결정
        // .isPressed는 키를 누르고 있는 동안 계속 '참(true)'을 반환합니다.
        if (keyboard.wKey.isPressed) moveZ = 1;  // 앞
        if (keyboard.sKey.isPressed) moveZ = -1; // 뒤
        if (keyboard.aKey.isPressed) moveX = -1; // 왼쪽
        if (keyboard.dKey.isPressed) moveX = 1;  // 오른쪽
        if (keyboard.leftShiftKey.wasPressedThisFrame) speed = 8f; //달리기
        if (keyboard.leftShiftKey.wasReleasedThisFrame) speed = 5f;

        // 6. 대각선 이동 시 속도가 빨라지는 것을 방지 (정규화)
        Vector3 moveDirection = new Vector3(moveX, 0, moveZ).normalized;

        // 7. 실제로 캐릭터 위치를 옮김
        // Time.deltaTime을 곱해야 컴퓨터 성능에 상관없이 일정한 속도로 움직입니다.
        transform.Translate(moveDirection * speed * Time.deltaTime);
    }
}
