using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{

    private bool isSelecting = false;

    private Vector3 mousePosition;

    private List<ISelectable> unitSelected;

    private void Awake()
    {
        unitSelected = new List<ISelectable>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Deselect();
            isSelecting = true;
            mousePosition = Input.mousePosition;
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
            Physics.Raycast(ray, out hitPoint);
            MoveUnits(hitPoint.point);
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
            List<ISelectable> units = GameController.instance.GetISelectableRealization();
            foreach (ISelectable unit in units)
            {
                Vector2 pos = new Vector2(Camera.main.WorldToScreenPoint(unit.Positions).x, Screen.height - Camera.main.WorldToScreenPoint(unit.Positions).y);
                if (rect.Contains(pos))
                {
                    if (unitSelected.Count == 0) { unitSelected.Add(unit); }
                    else if (!CheckUnit(unit)) { unitSelected.Add(unit); }
                }
            }
        }
    }

    private bool CheckUnit(ISelectable unit)
    {
        bool result = false;
        foreach (ISelectable im in unitSelected)
        {
            if (im == unit) result = true;
        }
        return result;
    }

    private void Select()
    {
        if (unitSelected.Count > 0)
        {
            if (unitSelected.Count > 0)
            {
                foreach (ISelectable im in unitSelected)
                {
                    im.Indicator = true;
                }
            }
        }
    }

    private void Deselect()
    {
        if (unitSelected.Count > 0)
        {
            foreach (ISelectable im in unitSelected)
            {
                im.Indicator = false;
            }
        }
    }

    private void MoveUnits(Vector3 pos)
    {
        Vector3[,] positions;
        int sqrt;
        positions = CalculateMovePosition.Calulate(unitSelected.Count, out sqrt);
        int count = 0;
        for (int i = 0; i < sqrt; i++)
        {
            for (int j = 0; j < sqrt; j++)
            {
                if (count == unitSelected.Count) break;
                unitSelected[count].referenceIMove.MoveTo(pos + positions[i, j]);
                count++;
            }
        }
    }
}
