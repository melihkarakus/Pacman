using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

[RequireComponent(typeof(Movement))]
public class Pacman : MonoBehaviour
{
    
    public Movement movement { get; private set; } // Pacman'in hareketini sa�layan Movement bile�eni.

    private void Awake()
    {
        this.movement = GetComponent<Movement>(); // Movement bile�enine eri�im sa�lar.
    }

    private void Update()
    {
        // Kullan�c�n�n yukar� ok veya W tu�una bast���nda...
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            this.movement.SetDirection(Vector2.up); // Hareket y�n�n� yukar� olarak ayarlar.
        }
        // Kullan�c�n�n a�a�� ok veya S tu�una bast���nda...
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            this.movement.SetDirection(Vector2.down); // Hareket y�n�n� a�a�� olarak ayarlar.
        }
        // Kullan�c�n�n sol ok veya A tu�una bast���nda...
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            this.movement.SetDirection(Vector2.left); // Hareket y�n�n� sola olarak ayarlar.
        }
        // Kullan�c�n�n sa� ok veya D tu�una bast���nda...
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            this.movement.SetDirection(Vector2.right); // Hareket y�n�n� sa�a olarak ayarlar.
        }

        // Pacman'in y�n�ne g�re d�nme a��s�n� hesaplar.
        float angle = Mathf.Atan2(this.movement.direction.y, this.movement.direction.x);

        // Pacman'in d�nme a��s�n� g�nceller.
        this.transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
    }
    public void ResetState()
    {
        this.movement.ResetState();
        this.gameObject.SetActive(true);
    }
}
