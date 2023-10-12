using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GetKeyCodeManager : MonoBehaviour
{
    float axisHorx;
    float axisVerz;
    bool walkDown;
    bool JumpDown;
    bool iDown;
    bool sDown1;
    bool sDown2;
    bool sDown3;
    bool fDown;
    bool gDown;
    bool rDown;
    public float _axisHorx { get { return axisHorx; } }
    public float _axisVerz { get { return axisVerz; } }
    public bool _walkDown { get { return walkDown; } }
    public bool _JumpDown { get { return JumpDown; } }
    public bool _iDown { get { return iDown; } }
    public bool _sDown1 { get { return sDown1; } }
    public bool _sDown2 { get { return sDown2; } }
    public bool _sDown3 { get { return sDown3; } }
    public bool _fDown { get { return fDown; } }
    public bool _gDown { get { return gDown; } }
    public bool _rDown { get { return rDown; } }
    private void Update()
    {
        GetInput();
    }
    public void GetInput()
    {
        axisHorx = Input.GetAxisRaw("Horizontal");
        axisVerz = Input.GetAxisRaw("Vertical");
        walkDown = Input.GetButton("Walk");
        JumpDown = Input.GetButtonDown("Jump");
        fDown = Input.GetButton("Fire1");
        gDown = Input.GetButtonDown("Fire2");
        rDown = Input.GetButtonDown("Reload");
        iDown = Input.GetButtonDown("Interation");
        sDown1 = Input.GetButtonDown("sDown1");
        sDown2 = Input.GetButtonDown("sDown2");
        sDown3 = Input.GetButtonDown("sDown3");
    }
}
