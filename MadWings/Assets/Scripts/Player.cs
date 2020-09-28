using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Controller2D))]
public class Player : MonoBehaviour
{
	[SerializeField] private float hp;
	public float moveSpeed;
	public static Player instance;

	private Touch touch;
    private Vector2 offset;
    private Transform transf;
    private Controller2D controller2D;


    private void Awake()
    {
	    if (instance == null)
	    {
		    instance = this;
	    }
	    else
	    {
		    Destroy(gameObject);
	    }

        controller2D = GetComponent<Controller2D>();
        transf = GetComponent<Transform>();
        offset = transf.position;
    }

    private void Update()
    {
        GetInput();
    }

    private void FixedUpdate()
    {
        controller2D.MovePosition(offset);
    }

    private void GetInput()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                offset = new Vector2(
                    transf.position.x + touch.deltaPosition.x * moveSpeed,
                    transf.position.y + touch.deltaPosition.y * moveSpeed);
            }
        }
    }

    public void GetDamage(int damage)
    {
	    hp -= damage;

	    if (hp <= 0)
	    {
		    Destruction();
	    }
    }

    public void Destruction()
    {
	    Destroy(gameObject); //TODO: сделать перерождение / завершение уровня
    }

    public void OnTriggerEnter2D(Collider2D coll)
    {
	    switch (coll.tag)
	    {
		    case "Enemy":
			    GetDamage(coll.GetComponent<Enemy>().damage);
			    break;

		    case "Bonus":
			    var bonus = coll.GetComponent<Bonus>();

			    switch (bonus.bonusType)
			    {
				    case BonusType.IncreaseAttack:
					    //GetComponent<Gun>().fireType++; //TODO: переделать

                        break;
				    case BonusType.Health:
					    break;
				    case BonusType.KillAll:
					    break;
			    }

			    bonus.DespawnBonus();
			    break;
	    }
    }
}
	