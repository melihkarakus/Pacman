using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Oyunda bulunan hayaletleri temsil eden dizi.
    public Ghost[] ghosts;
    // Oyundaki Pacman karakterini temsil eden de�i�ken.
    public Pacman pacman;
    // Oyundaki yemleri (pellet) i�eren transform nesnesi.
    public Transform pellets;
    // Hayalet puanlar� i�in �arpan de�eri.
    public int ghostMultiplier { get; private set; } = 1;
    // Oyuncunun skorunu tutan �zellik.
    public int score { get; private set; }
    // Oyuncunun can say�s�n� tutan �zellik.
    public int lives { get; private set; }

    private void Start() // Oyun ba�lad���nda �a�r�lan fonksiyon.
    {
        NewGame(); // Oyun ba�lad���nda yeni bir oyunu ba�lat�r.
    }

    private void Update()
    {
        // Oyuncunun can� kalmad���nda ve herhangi bir tu�a bas�ld���nda...
        if (this.lives <= 0 && Input.anyKeyDown)
        {
            NewGame(); // ...yeni bir oyun ba�lat�l�r.
        }
    }

    private void NewGame()
    {
        SetScore(0); // Skor s�f�rlan�r.
        SetLives(3); // Can say�s� 3 olarak ayarlan�r.
        NewRound(); // Yeni bir oyun b�l�m� ba�lat�l�r.
    }

    private void NewRound() // Yeni bir oyun b�l�m�n� ba�latan fonksiyon.
    {
        foreach (Transform pellet in this.pellets)
        {
            pellet.gameObject.SetActive(true); // Yemler aktif hale getirilir.
        }
        ResetState(); // Oyun durumlar� s�f�rlan�r.
    }

    private void ResetState() // Oyun durumlar�n� s�f�rlayan fonksiyon.
    {
        ResetGhostMultiplier();
        for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].ResetState(); // Hayaletler tekrar aktif hale getirilir.
        }
        this.pacman.ResetState(); // Pacman karakteri aktif hale getirilir.
    }

    private void GameOver()
    {
        for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].gameObject.SetActive(false); // Hayaletler oyun bitti�inde devre d��� b�rak�l�r.
        }
        this.pacman.gameObject.SetActive(false); // Pacman karakteri oyun bitti�inde devre d��� b�rak�l�r.
    }

    private void SetScore(int score)
    {
        this.score = score; // Skor ayarlan�r.
    }

    private void SetLives(int lives)
    {
        this.lives = lives; // Can say�s� ayarlan�r.
    }

    public void GhostEaten(Ghost ghost)
    {
        int points = ghost.points * this.ghostMultiplier;
        SetScore(this.score + points); // Oyuncu hayalet yedi�inde puan� g�ncellenir.
        this.ghostMultiplier++;
    }

    public void PacmanEaten()
    {
        this.pacman.gameObject.SetActive(false); // Pacman karakteri �ld���nde devre d��� b�rak�l�r.

        SetLives(this.lives - 1); // Can say�s� bir azalt�l�r.

        if (this.lives > 0)
        {
            Invoke(nameof(ResetState), 3.0f); // Can say�s� s�f�rdan b�y�kse, bir s�re sonra oyun durumlar� s�f�rlan�r.
        }
        else
        {
            GameOver(); // Can say�s� s�f�rsa oyun biter.
        }
    }

    public void PelletEaten(Pellet pellet)
    {
        pellet.gameObject.SetActive(false);
        SetScore(this.score + pellet.point);

        if (!HasRemainingPellet())
        {
            this.pacman.gameObject.SetActive(false);

            Invoke(nameof(NewRound), 3.0f); // T�m pelletler yendiyse, yeni bir oyun b�l�m� ba�lat�l�r.
        }
    }

    public void PowerPelletEaten(PowerPellet pellet)
    {
        for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].frightened.Enable(pellet.duraction);
        }

        PelletEaten(pellet);
        CancelInvoke(); // Invoke �a�r�lar�n� iptal eder.
        Invoke(nameof(ResetGhostMultiplier), pellet.duraction); // GhostMultiplier'� belirli bir s�re sonra s�f�rlar.
    }

    private bool HasRemainingPellet()
    {
        foreach (Transform pellet in this.pellets)
        {
            if (pellet.gameObject.activeSelf)
            {
                return true;
            }
        }
        return false;
    }

    private void ResetGhostMultiplier()
    {
        this.ghostMultiplier = 1; // GhostMultiplier'� s�f�rlar.
    }
}

