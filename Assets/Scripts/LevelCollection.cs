using UnityEngine;
using System.Collections;

public class LevelCollection : MonoBehaviour {
    public ExplosionManager em;

    public IEnumerator Level1() {
        while (true) {
            em.ExplodeRow(0);
            yield return new WaitForSeconds(0.3f);
            em.ExplodeRow(1);
            yield return new WaitForSeconds(0.3f);
            em.ExplodeRow(2);
            yield return new WaitForSeconds(0.3f);
            em.ExplodeRow(3);
            yield return new WaitForSeconds(0.3f);
            em.ExplodeRow(4);
            yield return new WaitForSeconds(1.2f);
        }
    }

    public IEnumerator Level2() {
        while (true) {
            em.ExplodeFourSquare(0);
            em.ExplodeFourSquare(1);
            em.ExplodeFourSquare(2);
            em.ExplodeFourSquare(3);
            yield return new WaitForSeconds(1.5f);
            em.ExplodePlus();
            yield return new WaitForSeconds(1.5f);            
        }
    }

    public IEnumerator Level3() {
        while (true) {
            em.ExplodeColumn(4);
            yield return new WaitForSeconds(0.7f);
            em.ExplodeColumn(3);
            yield return new WaitForSeconds(0.7f);
            em.ExplodeColumn(2);
            yield return new WaitForSeconds(0.7f);
            em.ExplodeColumn(1);
            yield return new WaitForSeconds(0.7f);
            em.ExplodeColumn(0);
            yield return new WaitForSeconds(0.7f);
        }
    }

    public IEnumerator Level4() {
        while (true) {
            em.ExplodeTile(0, 0);
            em.ExplodeTile(0, 4);
            em.ExplodeTile(4, 0);
            em.ExplodeTile(4, 4);
            yield return new WaitForSeconds(0.2f);
            em.ExplodeTile(1, 1);
            em.ExplodeTile(1, 3);
            em.ExplodeTile(3, 1);
            em.ExplodeTile(3, 3);
            yield return new WaitForSeconds(0.2f);
            em.ExplodeTile(2, 2);
            yield return new WaitForSeconds(2.0f);
        }
    }

    public IEnumerator Level5() {
        while (true) {
            em.ExplodeRow(0);
            em.ExplodeRow(4);
            yield return new WaitForSeconds(0.5f);
            em.ExplodeRow(1);
            em.ExplodeRow(3);
            yield return new WaitForSeconds(1.2f);
            em.ExplodeRow(2);
            yield return new WaitForSeconds(2.0f);
        }
    }

    public IEnumerator Level6() {
        while (true) {
            em.ExplodeTile(4, 0);
            yield return new WaitForSeconds(0.1f);
            em.ExplodeTile(3, 1);
            yield return new WaitForSeconds(0.1f);
            em.ExplodeTile(2, 2);
            yield return new WaitForSeconds(0.1f);
            em.ExplodeTile(1, 3);
            yield return new WaitForSeconds(0.1f);
            em.ExplodeTile(0, 4);
            yield return new WaitForSeconds(0.1f);
            em.ExplodeTile(4, 4);
            yield return new WaitForSeconds(0.1f);
            em.ExplodeTile(3, 3);
            yield return new WaitForSeconds(0.1f);
            em.ExplodeTile(2, 2);
            yield return new WaitForSeconds(0.1f);
            em.ExplodeTile(1, 1);
            yield return new WaitForSeconds(0.1f);
            em.ExplodeTile(0, 0);
            yield return new WaitForSeconds(0.3f);
            em.ExplodeTile(0, 1);
            em.ExplodeTile(0, 2);
            em.ExplodeTile(0, 3);
            em.ExplodeTile(4, 1);
            em.ExplodeTile(4, 2);
            em.ExplodeTile(4, 3);
            em.ExplodeTile(1, 0);
            em.ExplodeTile(2, 0);
            em.ExplodeTile(3, 0);
            em.ExplodeTile(1, 4);
            em.ExplodeTile(2, 4);
            em.ExplodeTile(3, 4);
            yield return new WaitForSeconds(3.0f);
        }
    }

    // TODO this level requires walls
    public IEnumerator Level7() {
        while (true) {
            em.ExplodeTile(0, 0);
            yield return new WaitForSeconds(1.0f);
        }
    }

    // TODO this level requires the vaccum
    public IEnumerator Level8() {
        while (true) {
            em.ExplodeTile(0, 0);
            yield return new WaitForSeconds(1.0f);
        }
    }
    
    public IEnumerator Level9() {
        while (true) {
            em.ExplodeRow(0);
            em.ExplodeRow(4);
            em.ExplodeColumn(0);
            em.ExplodeColumn(4);
            yield return new WaitForSeconds(1.0f);
            em.ExplodeRow(0);
            em.ExplodeTile(1, 1);
            em.ExplodeTile(1, 3);
            em.ExplodeTile(2, 2);
            yield return new WaitForSeconds(2.0f);
        }
    }

    // TODO this level requires walls
    public IEnumerator Level10() {
        while (true) {
            em.ExplodeTile(0, 0);
            yield return new WaitForSeconds(1.0f);
        }
    }

    // TODO this level requires walls
    public IEnumerator Level11() {
        while (true) {
            em.ExplodeTile(0, 0);
            yield return new WaitForSeconds(1.0f);
        }
    }

    public IEnumerator Level12() {
        while (true) {
            em.ExplodeTile(4, 2);
            yield return new WaitForSeconds(0.1f);
            em.ExplodeTile(3, 2);
            yield return new WaitForSeconds(0.1f);
            em.ExplodeTile(2, 2);
            yield return new WaitForSeconds(0.1f);
            em.ExplodeTile(1, 2);
            yield return new WaitForSeconds(0.1f);
            em.ExplodeTile(0, 2);
            yield return new WaitForSeconds(0.1f);
            em.ExplodeTile(0, 1);
            em.ExplodeTile(0, 3);
            yield return new WaitForSeconds(0.1f);
            em.ExplodeTile(0, 0);
            em.ExplodeTile(0, 4);
            yield return new WaitForSeconds(0.1f);
            em.ExplodeTile(1, 0);
            em.ExplodeTile(1, 4);
            yield return new WaitForSeconds(0.1f);
            em.ExplodeTile(2, 0);
            em.ExplodeTile(2, 4);
            yield return new WaitForSeconds(0.1f);
            em.ExplodeTile(3, 0);
            em.ExplodeTile(3, 4);
            yield return new WaitForSeconds(0.1f);
            em.ExplodeTile(4, 0);
            em.ExplodeTile(4, 4);
            yield return new WaitForSeconds(0.1f);
            em.ExplodeTile(4, 1);
            em.ExplodeTile(4, 3);
            yield return new WaitForSeconds(0.1f);
            em.ExplodeTile(3, 1);
            em.ExplodeTile(3, 3);
            yield return new WaitForSeconds(0.1f);
            em.ExplodeTile(2, 1);
            em.ExplodeTile(2, 3);
            yield return new WaitForSeconds(0.1f);
            em.ExplodeTile(1, 1);
            em.ExplodeTile(1, 3);
            yield return new WaitForSeconds(0.8f);
        }
    }

    // TODO this level requires walls
    public IEnumerator Level13() {
        while (true) {
            em.ExplodeTile(0, 0);
            yield return new WaitForSeconds(1.0f);
        }
    }

    public IEnumerator Level14() {
        while (true) {
            em.ExplodeTile(2, 4);
            yield return new WaitForSeconds(0.1f);
            em.ExplodeTile(2, 3);
            yield return new WaitForSeconds(0.1f);
            em.ExplodeTile(2, 2);
            yield return new WaitForSeconds(0.1f);
            em.ExplodeTile(2, 1);
            yield return new WaitForSeconds(0.1f);
            em.ExplodeTile(2, 0);
            yield return new WaitForSeconds(0.1f);
            em.ExplodeTile(0, 0);
            em.ExplodeTile(1, 0);
            em.ExplodeTile(3, 0);
            em.ExplodeTile(4, 0);
            yield return new WaitForSeconds(0.1f);
            em.ExplodeTile(0, 1);
            em.ExplodeTile(1, 1);
            em.ExplodeTile(3, 1);
            em.ExplodeTile(4, 1);
            yield return new WaitForSeconds(0.1f);
            em.ExplodeTile(0, 2);
            em.ExplodeTile(1, 2);
            em.ExplodeTile(3, 2);
            em.ExplodeTile(4, 2);
            yield return new WaitForSeconds(0.1f);
            em.ExplodeTile(0, 3);
            em.ExplodeTile(1, 3);
            em.ExplodeTile(3, 3);
            em.ExplodeTile(4, 3);
            yield return new WaitForSeconds(0.1f);
            em.ExplodeTile(0, 4);
            em.ExplodeTile(1, 4);
            em.ExplodeTile(3, 4);
            em.ExplodeTile(4, 4);
            yield return new WaitForSeconds(1.0f);
        }
    }

    public IEnumerator Level15() {
        while (true) {
            em.ExplodeTile(0, 4);
            em.ExplodeTile(4, 0);
            yield return new WaitForSeconds(0.1f);
            em.ExplodeTile(0, 3);
            em.ExplodeTile(4, 1);
            yield return new WaitForSeconds(0.1f);
            em.ExplodeTile(0, 2);
            em.ExplodeTile(4, 2);
            yield return new WaitForSeconds(0.1f);
            em.ExplodeTile(0, 1);
            em.ExplodeTile(4, 3);
            yield return new WaitForSeconds(0.1f);
            em.ExplodeTile(0, 0);
            em.ExplodeTile(4, 4);
            yield return new WaitForSeconds(0.1f);
            em.ExplodeTile(1, 0);
            em.ExplodeTile(3, 4);
            yield return new WaitForSeconds(0.1f);
            em.ExplodeTile(1, 1);
            em.ExplodeTile(3, 3);
            yield return new WaitForSeconds(0.1f);
            em.ExplodeTile(1, 2);
            em.ExplodeTile(3, 2);
            yield return new WaitForSeconds(0.1f);
            em.ExplodeTile(1, 3);
            em.ExplodeTile(3, 1);
            yield return new WaitForSeconds(0.1f);
            em.ExplodeTile(1, 4);
            em.ExplodeTile(3, 0);
            yield return new WaitForSeconds(0.1f);
            em.ExplodeTile(2, 4);
            em.ExplodeTile(2, 0);
            yield return new WaitForSeconds(0.1f);
            em.ExplodeTile(2, 3);
            em.ExplodeTile(2, 1);
            yield return new WaitForSeconds(0.1f);
            em.ExplodeTile(2, 2);
            yield return new WaitForSeconds(1.2f);
        }
    }
}
