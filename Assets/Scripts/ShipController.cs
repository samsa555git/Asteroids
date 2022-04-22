using UnityEngine;
using System.Collections;
 
public class ShipController : MonoBehaviour {
 
    float rotationSpeed = 100.0f;
    float thrustForce = 3f;
 
    public AudioClip crash;
    public AudioClip shoot;

    public AudioClip rocket;
 
    public GameObject bullet;
 
    public Sprite[] spriteArray;
    private GameController gameController;

    private bool flicker;
    private float flicktime;
 
    private float ShootTimer = 0;

    void Start(){
        // Get a reference to the game controller object and the script
        GameObject gameControllerObject =
            GameObject.FindWithTag ("GameController");
 
        gameController =
            gameControllerObject.GetComponent <GameController>();
    }
 
    void FixedUpdate () {
 
        // Rotate the ship if necessary
            transform.Rotate(0, 0, -Input.GetAxis("Horizontal")*
            rotationSpeed * Time.deltaTime);
 
        // Thrust the ship if necessary
            
            GetComponent<Rigidbody2D>().
            AddForce(transform.up * thrustForce *
            Input.GetAxis("Vertical"));
            
            if (Input.GetAxis("Vertical")>=0.1 && thrustForce>0) 

            

            {
             GetComponent<SpriteRenderer>().sprite=spriteArray[1];

             AudioSource.PlayClipAtPoint
                (rocket, Camera.main.transform.position, 0.07f);

            } 
            else if(thrustForce>0)
            {
            GetComponent<SpriteRenderer>().sprite=spriteArray[0];
            }

        // flicker. Thnx to Noora

        if (flicker) {
            GetComponent<SpriteRenderer>().enabled = !GetComponent<SpriteRenderer>().enabled;
            flicktime += Time.deltaTime; 
            if (flicktime>3){
                flicker = false;
                flicktime = 0;
                 GetComponent<SpriteRenderer>().enabled = true;
            }
            
        }

        //   Debug.Log(Input.GetAxis("Vertical"));
 
        // Has a bullet been fired. Vanha mouse0 setuppi
        // if (Input.GetMouseButtonDown (0))
        //    ShootBullet ();

        ShootTimer += Time.deltaTime;
        if (ShootTimer>=0.75 && Input.GetButton("Fire1"))
            
            {
                ShootBullet (); 
                ShootTimer = 0;
            }
 
    }
    
    // crash 
    void Crash(){
        transform.position = new Vector3 (0, 0, 0);
        thrustForce = 3;
        rotationSpeed = 100;
        GetComponent<SpriteRenderer>().sprite=spriteArray[0];
        GetComponent<TrailRenderer>().Clear();

        flicker = true; 
    }

    void changesprite(){
        GetComponent<SpriteRenderer>().sprite=spriteArray[3];
    }

    void OnTriggerEnter2D(Collider2D c){
 
        // Anything except a bullet is an asteroid
        if (c.gameObject.tag != "Bullet" && flicktime==0) {
 
            AudioSource.PlayClipAtPoint
                (crash, Camera.main.transform.position, 0.05f);
 
            // Move the ship to the centre of the screen
            
           // transform.position = new Vector3 (0, 0, 0); 
           // sprite change.
            Invoke("Crash", 1.5f);
            Invoke("changesprite", 1.0f);
            thrustForce = 0;
            rotationSpeed = 0;
            GetComponent<SpriteRenderer>().sprite=spriteArray[2];

            // Remove all velocity from the ship
            GetComponent<Rigidbody2D> ().
                velocity = new Vector3 (0, 0, 0);
 
            gameController.DecrementLives ();
        }
    }
 
    void ShootBullet(){
 
        // Spawn a bullet
        Instantiate(bullet,
            new Vector3(transform.position.x,transform.position.y, 0),
            transform.rotation);
 
        // Play a shoot sound
        AudioSource.PlayClipAtPoint (shoot, Camera.main.transform.position, 0.05f);
    
    }
}