﻿using UnityEngine;

public class PointInTime {
    public Vector3 position;
    public Quaternion rotation;

    public PointInTime(Vector3 position, Quaternion rotation) {
        this.position = position;
        this.rotation = rotation;
    }
}
