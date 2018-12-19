using UnityEngine;
using System.Collections;

public class CharacterPlayerStats : MonoBehaviour {

    [Range(0, 100)] public float health = 100;
    public float armor = 0;
    public readonly int maxHealth = 100;
    public readonly int maxArmor = 100;
    [Range(0, 100)] public int protectPrec = 0;
    public int faction;
    bool protection = false;

	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
    void Update () {
		health = Mathf.Clamp (health, 0, 100);
	}
    private void FixedUpdate()
    {
        if (protectPrec <= 0)
            protection = false;  
    }


}
