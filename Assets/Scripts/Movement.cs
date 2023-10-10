using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    public float speed = 8.0f; // Hareket h�z�.
    public float speedMultiplier = 1.0f; // H�z �arpan�.
    public Vector2 initialDirection; // Ba�lang�� y�nlendirmesi.
    public LayerMask obstacleLayer; // Engel katman�.

    public new Rigidbody2D rigidbody { get; private set; } // Hareketi sa�layan Rigidbody bile�eni.
    public Vector2 direction { get; private set; } // Ge�erli y�nlendirme.
    public Vector2 nextDirection { get; private set; } // Sonraki y�nlendirme.
    public Vector3 startingPosition { get; private set; } // Ba�lang�� pozisyonu.

    private void Awake()
    {
        this.rigidbody = GetComponent<Rigidbody2D>(); // Rigidbody bile�enine eri�im.
        this.startingPosition = this.transform.position; // Ba�lang�� pozisyonunu kaydet.
    }

    private void Start()
    {
        ResetState(); // Durumu s�f�rla.
    }

    public void ResetState()
    {
        this.speedMultiplier = 1.0f; // H�z �arpan�n� s�f�rla.
        this.direction = this.initialDirection; // Ba�lang�� y�nlendirmesini ayarla.
        this.nextDirection = Vector2.zero; // Sonraki y�nlendirmeyi s�f�rla.
        this.transform.position = this.startingPosition; // Pozisyonu ba�lang�� pozisyonuna ayarla.
        this.rigidbody.isKinematic = false; // Kinematik �zelli�i devre d��� b�rak.
        this.enabled = true; // Bu scripti etkinle�tir.
    }

    private void Update()
    {
        if (this.nextDirection != Vector2.zero)
        {
            SetDirection(this.nextDirection); // Sonraki y�nlendirmeyi ayarla.
        }
    }

    public void FixedUpdate()
    {
        Vector2 position = this.rigidbody.position; // Ge�erli pozisyon.
        Vector2 translation = direction * speed * speedMultiplier * Time.fixedDeltaTime; // Hareket �evirisi.

        this.rigidbody.MovePosition(position + translation); // Yeni pozisyonu g�ncelle.
    }

    public void SetDirection(Vector2 direction, bool forced = false)
    {
        if (forced || !Occupied(direction)) // E�er y�nlendirme zorlanm��sa veya y�nlendirme engelli de�ilse...
        {
            this.direction = direction; // Ge�erli y�nlendirmeyi ayarla.
            this.nextDirection = Vector2.zero; // Sonraki y�nlendirmeyi s�f�rla.
        }
        else
        {
            this.nextDirection = direction; // Aksi takdirde sonraki y�nlendirmeyi ayarla.
        }
    }

    public bool Occupied(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.BoxCast(this.transform.position, Vector2.one * 0.75f, 0.0f, direction, 1.5f, this.obstacleLayer); // Belirli bir y�nde engel kontrol�.
        return hit.collider != null; // E�er bir engel varsa true d�nd�r.
    }
}
