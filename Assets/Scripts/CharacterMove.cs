using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    [SerializeField] Transform _cam;
    [SerializeField] Inventory _inven;
    Animator _ani;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        _ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {

        //Debug.Log("카메라가 보는 방향을 월드 축기준으로 변경하면 :"+ _cam.transform.forward);
        transform.rotation = Quaternion.LookRotation(new Vector3(_cam.transform.forward.x, 0, _cam.transform.forward.z));
        float vX = Input.GetAxis("Horizontal");
        float vZ = Input.GetAxis("Vertical");

       
        float speed = 5f;
        if(Input.GetKey(KeyCode.LeftShift))
        {
            speed = 10f;
        }
        if(Input.GetMouseButton(0))
        {
            _ani.SetTrigger("Attack");
        }
        float vY = GetComponent<Rigidbody>().velocity.y;
        Vector3 forward = transform.forward;
        Vector3 right = transform.right;
        Vector3 v3 = forward * vZ + right * vX;
        Vector3 vYz = v3.normalized * speed;
        vYz.y += vY;
        GetComponent<Rigidbody>().velocity = vYz;
        //if (v3 != Vector3.zero)
        //{
        //    transform.rotation = Quaternion.LookRotation(v3);
        //}

        _ani.SetFloat("AxisX", vX);
        _ani.SetFloat("AxisX", vZ);
        _ani.SetFloat("moveValue", speed > 5? 2f : 1f);
    }

    public void AddCoin()
    {
        Item item = new Item();
        int count = Random.Range(1, 100);
        EItemType eType = (EItemType)Random.Range(1, (int)EItemType.Max - 1);
        item._eType = eType;
        item._Count = count;
        _inven.AddItem(item);
    }
}
