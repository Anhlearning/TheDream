using UnityEngine;

public class Projectile : MonoBehaviour {
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private GameObject _poofPrefab;
    private bool _isGhost;

    public void Init(Vector3 velocity) {
        _rb.AddForce(velocity, ForceMode.Impulse);
    }

}
