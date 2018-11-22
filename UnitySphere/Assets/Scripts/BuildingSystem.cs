using UnityEngine;

public class BuildingSystem: MonoBehaviour {

    enum BuildingObjectNames {
        Plane,
        Wall,
        Ramp
    }

    private bool isBuildingPlane = false;
    private bool isBuildingWall = false;
    private bool isBuildingRamp = false;

    private Ray ray;
    public Transform raycastStartPosition;
    public float buildingDistance = 3f;

    private GameObject gameObjectPreview;

    void Update() {

        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            EliminateGameObjectPreview();
            if (isBuildingPlane) isBuildingPlane = false;
            else {
                ray = new Ray();
                isBuildingPlane = true;
                isBuildingWall = false;
                isBuildingRamp = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            EliminateGameObjectPreview();
            if (isBuildingWall) isBuildingWall = false;
            else {
                isBuildingPlane = false;
                isBuildingWall = true;
                isBuildingRamp = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha3)) {
            EliminateGameObjectPreview();
            if (isBuildingRamp) isBuildingRamp = false;
            else {
                isBuildingPlane = false;
                isBuildingWall = false;
                isBuildingRamp = true;
            }
        }

        if (isBuildingPlane) {
            Building(BuildingObjectNames.Plane.ToString());
        }

        if (isBuildingWall) {
            Building(BuildingObjectNames.Wall.ToString());
        }

        if (isBuildingRamp) {
            Building(BuildingObjectNames.Ramp.ToString());
        }
    }

    private void Building(string BuildingObjectName) {
        ray.origin = raycastStartPosition.position;
        ray.direction = Camera.main.transform.forward;

        if (gameObjectPreview == null) {
            gameObjectPreview = Instantiate(Resources.Load("Prefabs/" + BuildingObjectName + "Preview")) as GameObject;
        }

        gameObjectPreview.transform.rotation = getObjectRotationDependingOnViewDirection();

        float xPosMod = ray.GetPoint(buildingDistance).x % 4f;
        float zPosMod = ray.GetPoint(buildingDistance).z % 4f;

        if (BuildingObjectName == BuildingObjectNames.Plane.ToString() || BuildingObjectName == BuildingObjectNames.Ramp.ToString()) {
            if (xPosMod >= 2f) xPosMod = ray.GetPoint(buildingDistance).x + (4f - xPosMod);
            else xPosMod = ray.GetPoint(buildingDistance).x - xPosMod;

            if (zPosMod > 2f) zPosMod = ray.GetPoint(buildingDistance).z + (4f - zPosMod) - 2f;
            else zPosMod = ray.GetPoint(buildingDistance).z - zPosMod + 2f;
        } else {
            if (gameObjectPreview.transform.rotation.eulerAngles.y == 0 || gameObjectPreview.transform.rotation.eulerAngles.y == 180) {
                if (xPosMod >= 2f) xPosMod = ray.GetPoint(buildingDistance).x + (4f - xPosMod);
                else xPosMod = ray.GetPoint(buildingDistance).x - xPosMod;

                if (zPosMod >= 2f) zPosMod = ray.GetPoint(buildingDistance).z + (4f - zPosMod);
                else zPosMod = ray.GetPoint(buildingDistance).z - zPosMod;
            } else if (gameObjectPreview.transform.rotation.eulerAngles.y == 90 || gameObjectPreview.transform.rotation.eulerAngles.y == 270) {

                if (xPosMod > 2f) xPosMod = ray.GetPoint(buildingDistance).x + (4f - xPosMod) - 2f;
                else xPosMod = ray.GetPoint(buildingDistance).x - xPosMod + 2f;

                if (zPosMod > 2f) zPosMod = ray.GetPoint(buildingDistance).z + (4f - zPosMod) - 2f;
                else zPosMod = ray.GetPoint(buildingDistance).z - zPosMod + 2f;
            }
        }

        float yPosMod = ray.GetPoint(buildingDistance).y % 3f;
        if (yPosMod >= 1.5f) yPosMod = ray.GetPoint(buildingDistance).y + (3f - yPosMod);
        else yPosMod = ray.GetPoint(buildingDistance).y - yPosMod;

        gameObjectPreview.transform.position = new Vector3(xPosMod, yPosMod, zPosMod);

        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            if (gameObjectPreview != null) {
                Instantiate(Resources.Load("Prefabs/" + BuildingObjectName), gameObjectPreview.transform.position, gameObjectPreview.transform.rotation);
            }
        }
    }

    private void EliminateGameObjectPreview() {
        Destroy(gameObjectPreview);
        gameObjectPreview = null;
    }

    private Quaternion getObjectRotationDependingOnViewDirection() {
        Vector3 aimingDir = Camera.main.transform.position - gameObjectPreview.transform.position;
        float angle = -Mathf.Atan2(aimingDir.z, aimingDir.x) * Mathf.Rad2Deg + 90.0f;
        angle = Mathf.Round(angle / 90.0f) * 90.0f;
        return Quaternion.AngleAxis(angle, Vector3.up);
    }
}
