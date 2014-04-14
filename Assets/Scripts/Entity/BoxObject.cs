using UnityEngine;
using System.Collections;

public class BoxObject {
	public int			x			{ get; set; }
	public int			y			{ get; set; }
	public int			index		{ get; set; }
	public GameObject	gameObject	{ get; set; }

	public BoxObject(int x, int y, int index) {
		this.x = x;
		this.y = y;
		this.index = index;
	}
}
