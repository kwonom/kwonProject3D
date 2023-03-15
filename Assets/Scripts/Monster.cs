using System.Collections;
using UnityEngine;

public enum EMonState
{
    None,
    Idle,
    Move,
    Attack,
    Die,
}
public class Monster : MonoBehaviour
{
    [SerializeField] GameObject _mon;
    [SerializeField] Transform _target;
    [SerializeField] GameObject _coin;
    EMonState _eState = EMonState.None;

    Animator _ani;
    int _hp = 0;

    float _serachDis = 10;
    float _attackDis = 2;
    float _speed = 3;
    // Start is called before the first frame update
    void Start()
    {
        _ani = _mon.GetComponent<Animator>();
        StartCoroutine(CoSpawn());
    }

    // Update is called once per frame
    void Update()
    {
        if (_eState == EMonState.Idle)
        {
            moveAndSerach();
        }
        if (_eState == EMonState.Attack)
        {
            followerAndAttack();
        }
    }

    void followerAndAttack()
    {
        _ani.Play("Attack");
        float dis = Vector3.Distance(_target.position, transform.position);
        if (dis > _attackDis)
        {
            if (_serachDis > dis)
            {
                _eState = EMonState.Idle;
            }
            else
            {
                Vector3 lookDir = _target.position - transform.position;
                transform.rotation = Quaternion.LookRotation(new Vector3(lookDir.x, 0, lookDir.z));
                transform.position = Vector3.MoveTowards(transform.position, _target.position, Time.deltaTime * _speed);
            }
        }


    }

    void moveAndSerach()
    {
        float dis = Vector3.Distance(_target.position, transform.position);
        if (dis < _serachDis)
        {
            if (dis < _attackDis)
            {
                _eState = EMonState.Attack;
            }
            else
            {
                Vector3 lookDir = _target.position - transform.position;
                transform.rotation = Quaternion.LookRotation(new Vector3(lookDir.x, 0, lookDir.z));
                transform.position = Vector3.MoveTowards(transform.position, _target.position, Time.deltaTime * _speed);
            }
        }
        else
        {
            _eState = EMonState.Move;
            StartCoroutine(CoRandomMove());
        }
    }



    IEnumerator CoRandomMove()
    {
        Vector3 randomDir = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
        transform.rotation = Quaternion.LookRotation(randomDir);

        yield return new WaitForSeconds(1f);

        Vector3 front = transform.position + transform.forward + new Vector3(0, 0.5f, 0);


        RaycastHit hit;
        Vector3 targetDir = transform.position + transform.forward * 2;

        bool canMove = false;
        if (!Physics.Raycast(front, transform.forward * 2, out hit, 2))
        {
            canMove = true;
        }
        if (canMove)
        {
            Debug.DrawRay(targetDir + new Vector3(0, 0.5f, 0), new Vector3(0, -2f, 0), Color.red, 5);

        }
        if (Physics.Raycast(targetDir + new Vector3(0, 0.5f, 0), new Vector3(0, -2f, 0), out hit))
        {
            while (Vector3.Distance(transform.position, targetDir) > 0.1f)
            {
                _ani.Play("Move");
                transform.position = Vector3.MoveTowards(transform.position, targetDir, Time.deltaTime * _speed);
                yield return null;
            }
        }
        yield return new WaitForSeconds(1f);
        _eState = EMonState.Idle;

    }

    public void hitted()
    {
        _hp--;
        if(_hp <= 0)
        {
            _ani.Play("Die");
            _eState = EMonState.Die;
        }
        else
        {
            _ani.Play("Hitted");
        }
    }

    public void DieEnd()
    {
        _mon.gameObject.SetActive(false);
        GameObject temp = Instantiate(_coin);
        temp.transform.position = transform.position;
    }



    void Spawn()
    {
        _hp = 5;
        _mon.SetActive(true);
        _eState = EMonState.Idle;
    }

    IEnumerator CoSpawn()
    {
        int rand = Random.Range(2, 5);
        yield return new WaitForSeconds(rand);
        Spawn();
    }
}
