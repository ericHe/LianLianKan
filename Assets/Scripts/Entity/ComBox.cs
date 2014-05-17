using UnityEngine;
using System.Collections;

public class ComBox : MonoBehaviour, IBox {
	public int		index		{ get; set; }
	public int		x			{ get; set; }
	public int		y			{ get; set; }
	// 17 冰块
	private int		m_ice;
	public int		ice {
		get {
			return m_ice;
		}
		set {
			m_ice = value;
			if(value > 0)
				iceSprite.sprite.name = "lianlian_"+value;
			else
				iceSprite.gameObject.SetActive(false);
		}
	}

	public bool		link		{ get; set; } //是否有链接
	public bool		check		{ get; set; } //是否检测过了
	public bool		fall		{ get; set; } //是否正在掉落

	public SpriteRenderer iceSprite;

	void start(){

	}

	public void FallDown(){
		fall = true;
		Rigidbody2D r = gameObject.AddComponent<Rigidbody2D>();
		r.gravityScale = 0.5f;
		float xF = Random.Range(-60f, 60f);
		float yF = Random.Range(30f, 60f);
		r.AddForce(new Vector2(xF, yF));
	}

	public void Explode(){
		if(ice > 0){
			ice = 0;
		} else {
			BoxManager.GetInstance().RemoveBox(this);
			ParticleSystem ps = ExpParticleFactory.Instance().CreateExpParticel(transform.position);
			//ps.transform.parent = transform.parent;
			Destroy(gameObject);
			ps.Play();
		}
	}
}
