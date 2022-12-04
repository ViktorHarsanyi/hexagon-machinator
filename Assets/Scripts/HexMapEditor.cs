using UnityEngine;
using UnityEngine.EventSystems;

public class HexMapEditor : MonoBehaviour {

	public Color[] colors;

	public HexGrid hexGrid;

    public CellUnit unitPrefab;

    int activeElevation;

	Color activeColor;

	public void SelectColor (int index) {
		activeColor = colors[index];
	}

	public void SetElevation (float elevation) {
		activeElevation = (int)elevation;
	}

	void Awake () {

		SelectColor(0);
		GenerateCells();
	}

	void Update () {
	

        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (Input.GetMouseButton(0))
            {
                HandleInput();
                return;
            }
            if (Input.GetMouseButton(1))
            {
                CreateCellUnit();
                return;
            }
        }
    }

	void HandleInput () {


        HexCell currentCell = GetCellUnderCursor();
        if (currentCell)
        {
		EditCell(currentCell);
		}
     
    }

    void CreateCellUnit()
    {
        HexCell cell = GetCellUnderCursor();
        if (cell)
        {
            CellUnit unit = Instantiate(unitPrefab);
            unit.transform.SetParent(hexGrid.transform, false);
            unit.Location = cell;
        }
    }

    HexCell GetCellUnderCursor()
    {
        Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(inputRay, out hit))
        {

            var cell = hexGrid.GetCell(hit.point);
            return (cell.color == colors[0]) ? null : cell;
        
        }
        return null;
    }

    void GenerateCells() {

		

		foreach(HexCell cell in hexGrid.cells)
		{

			//float xc = Mathf.PerlinNoise(cell.coordinates.X, cell.coordinates.Y);

			var elevation = Random.Range(0, 8);

            if(cell.coordinates.Z == 0 || cell.coordinates.X == cell.coordinates.Y)
            {
                elevation= 0;
            }

            cell.Elevation = elevation;

			// kék az első
			cell.color = elevation == 0f ? colors[0] : colors[Random.Range(1,colors.Length-1)];
			
			hexGrid.Refresh();


		}

	}

	void EditCell (HexCell cell) {
		cell.color = activeColor;
		cell.Elevation = activeElevation;
		hexGrid.Refresh();
	}
}