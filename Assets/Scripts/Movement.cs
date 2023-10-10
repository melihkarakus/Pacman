using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    public float speed = 8.0f; // Hareket hýzý.
    public float speedMultiplier = 1.0f; // Hýz çarpaný.
    public Vector2 initialDirection; // Baþlangýç yönlendirmesi.
    public LayerMask obstacleLayer; // Engel katmaný.

    public new Rigidbody2D rigidbody { get; private set; } // Hareketi saðlayan Rigidbody bileþeni.
    public Vector2 direction { get; private set; } // Geçerli yönlendirme.
    public Vector2 nextDirection { get; private set; } // Sonraki yönlendirme.
    public Vector3 startingPosition { get; private set; } // Baþlangýç pozisyonu.

    private void Awake()
    {
        this.rigidbody = GetComponent<Rigidbody2D>(); // Rigidbody bileþenine eriþim.
        this.startingPosition = this.transform.position; // Baþlangýç pozisyonunu kaydet.
    }

    private void Start()
    {
        ResetState(); // Durumu sýfýrla.
    }

    public void ResetState()
    {
        this.speedMultiplier = 1.0f; // Hýz çarpanýný sýfýrla.
        this.direction = this.initialDirection; // Baþlangýç yönlendirmesini ayarla.
        this.nextDirection = Vector2.zero; // Sonraki yönlendirmeyi sýfýrla.
        this.transform.position = this.startingPosition; // Pozisyonu baþlangýç pozisyonuna ayarla.
        this.rigidbody.isKinematic = false; // Kinematik özelliði devre dýþý býrak.
        this.enabled = true; // Bu scripti etkinleþtir.
    }

    private void Update()
    {
        if (this.nextDirection != Vector2.zero)
        {
            SetDirection(this.nextDirection); // Sonraki yönlendirmeyi ayarla.
        }
    }

    public void FixedUpdate()
    {
        Vector2 position = this.rigidbody.position; // Geçerli pozisyon.
        Vector2 translation = direction * speed * speedMultiplier * Time.fixedDeltaTime; // Hareket çevirisi.

        this.rigidbody.MovePosition(position + translation); // Yeni pozisyonu güncelle.
    }

    public void SetDirection(Vector2 direction, bool forced = false)
    {
        if (forced || !Occupied(direction)) // Eðer yönlendirme zorlanmýþsa veya yönlendirme engelli deðilse...
        {
            this.direction = direction; // Geçerli yönlendirmeyi ayarla.
            this.nextDirection = Vector2.zero; // Sonraki yönlendirmeyi sýfýrla.
        }
        else
        {
            this.nextDirection = direction; // Aksi takdirde sonraki yönlendirmeyi ayarla.
        }
    }

    public bool Occupied(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.BoxCast(this.transform.position, Vector2.one * 0.75f, 0.0f, direction, 1.5f, this.obstacleLayer); // Belirli bir yönde engel kontrolü.
        return hit.collider != null; // Eðer bir engel varsa true döndür.
    }
}
