using UnityEngine;
using System.Collections;

public class LevelCollection : MonoBehaviour {
    public ExplosionManager em;

    public IEnumerator LevelOne() {
        while (true) {
            em.TriggerTileRow(0);
            yield return new WaitForSeconds(0.3f);
            em.TriggerTileRow(1);
            yield return new WaitForSeconds(0.3f);
            em.TriggerTileRow(2);
            yield return new WaitForSeconds(0.3f);
            em.TriggerTileRow(3);
            yield return new WaitForSeconds(0.3f);
            em.TriggerTileRow(4);
            yield return new WaitForSeconds(1.2f);
        }
    }

    public IEnumerator LevelTwo() {
        while (true) {
            em.TriggerFourSquare(0);
            em.TriggerFourSquare(1);
            em.TriggerFourSquare(2);
            em.TriggerFourSquare(3);
            yield return new WaitForSeconds(1.5f);
            em.TriggerPlus();
            yield return new WaitForSeconds(1.5f);            
        }
    }

    public IEnumerator LevelThree() {
        while (true) {
            em.TriggerTileColumn(4);
            yield return new WaitForSeconds(0.7f);
            em.TriggerTileColumn(3);
            yield return new WaitForSeconds(0.7f);
            em.TriggerTileColumn(2);
            yield return new WaitForSeconds(0.7f);
            em.TriggerTileColumn(1);
            yield return new WaitForSeconds(0.7f);
            em.TriggerTileColumn(0);
            yield return new WaitForSeconds(0.7f);
        }
    }

    public IEnumerator LevelFour() {
        while (true) {
            em.TriggerTile(0, 0);
            em.TriggerTile(0, 4);
            em.TriggerTile(4, 0);
            em.TriggerTile(4, 4);
            yield return new WaitForSeconds(0.2f);
            em.TriggerTile(1, 1);
            em.TriggerTile(1, 3);
            em.TriggerTile(3, 1);
            em.TriggerTile(3, 3);
            yield return new WaitForSeconds(0.2f);
            em.TriggerTile(2, 2);
            yield return new WaitForSeconds(2.0f);
        }
    }

    public IEnumerator LevelFive() {
        while (true) {
            
        }
    }
}
