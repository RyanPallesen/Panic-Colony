﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    [SerializeField, Tooltip("The amount of rotation")] private float m_rotationAmount = 0.0f;
    [SerializeField, Range(0, 99), Tooltip("The speed at which it will rotate")] private float m_rotationSpeed = 0.0f;

    private Vector3 m_pivotPoint = Vector3.zero;
    private int m_rotatedPercent = 0;
    private bool m_rotating = false, m_rotated = false;
    private float m_totalSpeed = 0.0f;


    #region Jon, i love you.. but what why would you call this magic number? B
    /*
                                                /
                        _,.------....___,.' ',.-.
                     ,-'          _,.--"        |
                   ,'         _.-'              .
                  /   ,     ,'                   `
                 .   /     /                     ``.
                 |  |     .                       \.\
       ____      |___._.  |       __               \ `.
     .'    `---""       ``"-.--"'`  \               .  \
    .  ,            __               `              |   .
    `,'         ,-"'  .               \             |    L
   ,'          '    _.'                -._          /    |
  ,`-.    ,".   `--'                      >.      ,'     |
 . .'\'   `-'       __    ,  ,-.         /  `.__.-      ,'                SQUIRTLE SQUAD!
 ||:, .           ,'  ;  /  / \ `        `.    .      .'/
 j|:D  \          `--'  ' ,'_  . .         `.__, \   , /
/ L:_  |                 .  "' :_;                `.'.'
.    ""'                  """""'                    V
 `.                                 .    `.   _,..  `
   `,_   .    .                _,-'/    .. `,'   __  `
    ) \`._        ___....----"'  ,'   .'  \ |   '  \  .
   /   `. "`-.--"'         _,' ,'     `---' |    `./  |
  .   _  `""'--.._____..--"   ,             '         |
  | ." `. `-.                /-.           /          ,
  | `._.'    `,_            ;  /         ,'          .
 .'          /| `-.        . ,'         ,           ,
 '-.__ __ _,','    '`-..___;-...__   ,.'\ ____.___.'
 `"^--'..'   '-`-^-'"--    `-^-'`.''"""""`.,^.`.--'
     */
    #endregion



    private void Start()
    {
        m_rotated = false;
        m_rotating = false;
        m_pivotPoint = transform.position + (transform.right * (transform.localScale.x / 2f) * -1f);
        m_totalSpeed = 100 - m_rotationSpeed;
    }

    private void Update()
    {
        m_totalSpeed = 100 - m_rotationSpeed;
        if (m_rotatedPercent >= m_totalSpeed)
        {
            m_rotating = false;
        }
    }

    public void Rotate()
    {
        if (!m_rotating)
        {
            m_rotatedPercent = 0;
            m_rotating = true;
            StartCoroutine(Rotating());
        }
    }

    IEnumerator Rotating()
    {
        yield return new WaitForSeconds(0.01f);
        int rotationDirection = (m_rotated) ? -1 : 1;
        transform.RotateAround(m_pivotPoint, Vector3.up, (rotationDirection * m_rotationAmount) / m_totalSpeed);
        m_rotatedPercent++;
        if (m_rotatedPercent < m_totalSpeed)
        {
            StartCoroutine(Rotating());
        }
        else
        {
            m_rotated = (m_rotated) ? false : true;
        }
    }
}
