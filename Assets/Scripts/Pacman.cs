using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

[RequireComponent(typeof(Movement))]
public class Pacman : MonoBehaviour
{
    
    public Movement movement { get; private set; } // Pacman'in hareketini saðlayan Movement bileþeni.

    private void Awake()
    {
        this.movement = GetComponent<Movement>(); // Movement bileþenine eriþim saðlar.
    }

    private void Update()
    {
        // Kullanýcýnýn yukarý ok veya W tuþuna bastýðýnda...
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            this.movement.SetDirection(Vector2.up); // Hareket yönünü yukarý olarak ayarlar.
        }
        // Kullanýcýnýn aþaðý ok veya S tuþuna bastýðýnda...
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            this.movement.SetDirection(Vector2.down); // Hareket yönünü aþaðý olarak ayarlar.
        }
        // Kullanýcýnýn sol ok veya A tuþuna bastýðýnda...
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            this.movement.SetDirection(Vector2.left); // Hareket yönünü sola olarak ayarlar.
        }
        // Kullanýcýnýn sað ok veya D tuþuna bastýðýnda...
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            this.movement.SetDirection(Vector2.right); // Hareket yönünü saða olarak ayarlar.
        }

        // Pacman'in yönüne göre dönme açýsýný hesaplar.
        float angle = Mathf.Atan2(this.movement.direction.y, this.movement.direction.x);

        // Pacman'in dönme açýsýný günceller.
        this.transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
    }
    public void ResetState()
    {
        this.movement.ResetState();
        this.gameObject.SetActive(true);
    }
}
