using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GamePlayer : MonoBehaviour
{
    public float horizontalInput;
    public float verticalInput;
    public float speed_x = 150.0f;
    public float speed_y = 3.0f;
    public PhotonView photonView;
    public GameObject gun;
    public Transform bulletSpawnPoint;
    public Rigidbody rbody;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    { 
        if (photonView.IsMine)
        {
            
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Rotate(0, horizontalInput * speed_x * Time.deltaTime, 0);
        
        verticalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.forward * verticalInput * speed_y * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            photonView.RPC("Fire", RpcTarget.All);
        }
    }
    }
    [PunRPC]
    public void Fire(PhotonMessageInfo info)
    {
        Debug.Log("Firing!");
        float lag = (float)(PhotonNetwork.Time - info.SentServerTime);
    }
}
