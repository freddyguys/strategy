using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    public GameObject moveToEffect;
    public GameObject attackEffect;


    private bool isSelecting = false;

    private Vector3 mousePosition;

    private List<SoldierController> unitSelected;

    private void Awake()
    {
        unitSelected = new List<SoldierController>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Deselect();
            isSelecting = true;
            mousePosition = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "GoodGuy") { isSelecting = false; unitSelected.Add(hit.collider.transform.GetChild(0).GetComponent<SoldierController>()); Select(); }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            isSelecting = false;
            Select();
        }
        if (Input.GetMouseButtonDown(1) && unitSelected.Count > 0)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitPoint;
            if (Physics.Raycast(ray, out hitPoint))
            {
                SoldierController soldier = null;
                if (hitPoint.collider.tag == "BadGuy") { soldier = hitPoint.collider.transform.GetChild(0).GetComponent<SoldierController>(); ShowEffect(hitPoint.point, attackEffect); } else { ShowEffect(hitPoint.point, moveToEffect); }
                MoveUnits(hitPoint.point, soldier);
                Deselect(); unitSelected.Clear();
            }
        }
    }

    private void OnGUI()
    {
        if (isSelecting)
        {
            unitSelected.Clear();
            var rect = SelectedArea.GetScreenrect(mousePosition, Input.mousePosition);
            SelectedArea.DrawScreenRect(rect, new Color(0.8f, 0.8f, 0.95f, 0.25f));
            SelectedArea.DrawScreenRectBorder(rect, 2, new Color(0.8f, 0.8f, 0.95f, 0.25f));
            List<SoldierController> soldiers = GameController.instance.GetSoldiers();
            foreach (SoldierController soldier in soldiers)
            {
                Vector2 pos = new Vector2(Camera.main.WorldToScreenPoint(soldier.GetPositions()).x, Screen.height - Camera.main.WorldToScreenPoint(soldier.GetPositions()).y);
                if (rect.Contains(pos))
                {
                    if (unitSelected.Count == 0) { unitSelected.Add(soldier); }
                    else if (!CheckUnit(soldier)) { unitSelected.Add(soldier); }
                }
            }
        }
    }

    private bool CheckUnit(SoldierController unit)
    {
        bool result = false;
        foreach (SoldierController soldier in unitSelected)
        {
            if (soldier == unit) result = true;
        }
        return result;
    }

    private void Select()
    {
        if (unitSelected.Count > 0)
        {
            foreach (SoldierController soldier in unitSelected)
            {
                soldier.SelectInterface.Indicator = true;
            }
        }
    }

    private void Deselect()
    {
        if (unitSelected.Count > 0)
        {
            foreach (SoldierController soldier in unitSelected)
            {
                soldier.SelectInterface.Indicator = false;
            }
        }
    }

    private void MoveUnits(Vector3 pos, SoldierController soldier = null)
    {
        Vector3[,] positions;
        int sqrt;
        List<SoldierController> myInits = new List<SoldierController>();
        foreach (SoldierController unit in unitSelected)
        {
            if (unit.SoldierInterface.Tag == TeamTag.GoodGuy) myInits.Add(unit);
        }
        positions = CalculateMovePosition.Calulate(myInits.Count, out sqrt);
        int count = 0;
        for (int i = 0; i < sqrt; i++)
        {
            for (int j = 0; j < sqrt; j++)
            {
                if (count == myInits.Count) break;
                if (soldier != null) myInits[count].AttackInterface.AttackTarget(soldier);
                else myInits[count].MoveInterface.MoveTo(pos + positions[i, j]);
                count++;
            }
        }
    }

    private void ShowEffect(Vector3 point, GameObject effectObj)
    {
        Vector3 pos = point;
        pos.y = 0.6f;
        effectObj.transform.position = pos;
        effectObj.GetComponent<ParticleSystem>().Play();
    }
}
