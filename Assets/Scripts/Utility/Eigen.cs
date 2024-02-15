using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class Eigen {
    #region Swizzling

    #region To Vector2
    #region From Vector2
    public static Vector2 xy(this Vector2 v) {
        return v;
    }
    public static Vector2 yx(this Vector2 v) {
        return new Vector2(v.y, v.x);
    }
    #endregion
    #region From Vector3
    public static Vector2 xx(this Vector3 v) {
        return new Vector2(v.x, v.x);
    }
    public static Vector2 xy(this Vector3 v) {
        return new Vector2(v.x, v.y);
    }
    public static Vector2 xz(this Vector3 v) {
        return new Vector2(v.x, v.z);
    }
    public static Vector2 yx(this Vector3 v) {
        return new Vector2(v.y, v.x);
    }
    public static Vector2 yy(this Vector3 v) {
        return new Vector2(v.y, v.y);
    }
    public static Vector2 yz(this Vector3 v) {
        return new Vector2(v.y, v.z);
    }
    public static Vector2 zx(this Vector3 v) {
        return new Vector2(v.z, v.x);
    }
    public static Vector2 zy(this Vector3 v) {
        return new Vector2(v.z, v.y);
    }
    public static Vector2 zz(this Vector3 v) {
        return new Vector2(v.z, v.z);
    }
    #endregion
    #region From Vector4
    public static Vector2 xx(this Vector4 v) {
        return new Vector2(v.x, v.x);
    }
    public static Vector2 xy(this Vector4 v) {
        return new Vector2(v.x, v.y);
    }
    public static Vector2 xz(this Vector4 v) {
        return new Vector2(v.x, v.z);
    }
    public static Vector2 xw(this Vector4 v) {
        return new Vector2(v.x, v.w);
    }
    public static Vector2 yx(this Vector4 v) {
        return new Vector2(v.y, v.x);
    }
    public static Vector2 yy(this Vector4 v) {
        return new Vector2(v.y, v.y);
    }
    public static Vector2 yz(this Vector4 v) {
        return new Vector2(v.y, v.z);
    }
    public static Vector2 yw(this Vector4 v) {
        return new Vector2(v.y, v.w);
    }
    public static Vector2 zx(this Vector4 v) {
        return new Vector2(v.z, v.x);
    }
    public static Vector2 zy(this Vector4 v) {
        return new Vector2(v.z, v.y);
    }
    public static Vector2 zz(this Vector4 v) {
        return new Vector2(v.z, v.z);
    }
    public static Vector2 zw(this Vector4 v) {
        return new Vector2(v.z, v.w);
    }
    public static Vector2 wx(this Vector4 v) {
        return new Vector2(v.w, v.x);
    }
    public static Vector2 wy(this Vector4 v) {
        return new Vector2(v.w, v.y);
    }
    public static Vector2 wz(this Vector4 v) {
        return new Vector2(v.w, v.z);
    }
    public static Vector2 ww(this Vector4 v) {
        return new Vector2(v.w, v.w);
    }
    #endregion
    #endregion
    #region To Vector3
    #region From Vector2
    public static Vector3 xxx(this Vector2 v) {
        return new Vector3(v.x, v.x, v.x);
    }
    public static Vector3 xxy(this Vector2 v) {
        return new Vector3(v.x, v.x, v.y);
    }
    public static Vector3 xyx(this Vector2 v) {
        return new Vector3(v.x, v.y, v.x);
    }
    public static Vector3 xyy(this Vector2 v) {
        return new Vector3(v.x, v.y, v.y);
    }
    public static Vector3 yxx(this Vector2 v) {
        return new Vector3(v.y, v.x, v.x);
    }
    public static Vector3 yxy(this Vector2 v) {
        return new Vector3(v.y, v.x, v.y);
    }
    public static Vector3 yyx(this Vector2 v) {
        return new Vector3(v.y, v.y, v.x);
    }
    public static Vector3 yyy(this Vector2 v) {
        return new Vector3(v.y, v.y, v.y);
    }
    #endregion
    #region From Vector3
    public static Vector3 xxx(this Vector3 v) {
        return new Vector3(v.x, v.x, v.x);
    }
    public static Vector3 xxy(this Vector3 v) {
        return new Vector3(v.x, v.x, v.y);
    }
    public static Vector3 xxz(this Vector3 v) {
        return new Vector3(v.x, v.x, v.z);
    }
    public static Vector3 xyx(this Vector3 v) {
        return new Vector3(v.x, v.y, v.x);
    }
    public static Vector3 xzx(this Vector3 v) {
        return new Vector3(v.x, v.z, v.x);
    }
    public static Vector3 xyy(this Vector3 v) {
        return new Vector3(v.x, v.y, v.y);
    }
    public static Vector3 xyz(this Vector3 v) {
        return new Vector3(v.x, v.y, v.z);
    }
    public static Vector3 xzy(this Vector3 v) {
        return new Vector3(v.x, v.z, v.y);
    }
    public static Vector3 xzz(this Vector3 v) {
        return new Vector3(v.x, v.z, v.z);
    }
    public static Vector3 yxx(this Vector3 v) {
        return new Vector3(v.y, v.x, v.x);
    }
    public static Vector3 yxy(this Vector3 v) {
        return new Vector3(v.x, v.x, v.y);
    }
    public static Vector3 yxz(this Vector3 v) {
        return new Vector3(v.y, v.x, v.z);
    }
    public static Vector3 yyx(this Vector3 v) {
        return new Vector3(v.y, v.y, v.x);
    }
    public static Vector3 yzx(this Vector3 v) {
        return new Vector3(v.y, v.z, v.x);
    }
    public static Vector3 yyy(this Vector3 v) {
        return new Vector3(v.y, v.y, v.y);
    }
    public static Vector3 yyz(this Vector3 v) {
        return new Vector3(v.y, v.y, v.z);
    }
    public static Vector3 yzy(this Vector3 v) {
        return new Vector3(v.y, v.z, v.y);
    }
    public static Vector3 yzz(this Vector3 v) {
        return new Vector3(v.y, v.z, v.z);
    }
    public static Vector3 zxx(this Vector3 v) {
        return new Vector3(v.z, v.x, v.x);
    }
    public static Vector3 zxy(this Vector3 v) {
        return new Vector3(v.z, v.x, v.y);
    }
    public static Vector3 zxz(this Vector3 v) {
        return new Vector3(v.z, v.x, v.z);
    }
    public static Vector3 zyx(this Vector3 v) {
        return new Vector3(v.z, v.y, v.x);
    }
    public static Vector3 zzx(this Vector3 v) {
        return new Vector3(v.z, v.z, v.x);
    }
    public static Vector3 zyy(this Vector3 v) {
        return new Vector3(v.z, v.y, v.y);
    }
    public static Vector3 zyz(this Vector3 v) {
        return new Vector3(v.z, v.y, v.z);
    }
    public static Vector3 zzy(this Vector3 v) {
        return new Vector3(v.z, v.z, v.y);
    }
    public static Vector3 zzz(this Vector3 v) {
        return new Vector3(v.z, v.z, v.z);
    }
    #endregion
    #region From Vector4
    public static Vector3 xxx(this Vector4 v) {
        return new Vector3(v.x, v.x, v.x);
    }
    public static Vector3 xxy(this Vector4 v) {
        return new Vector3(v.x, v.x, v.y);
    }
    public static Vector3 xxz(this Vector4 v) {
        return new Vector3(v.x, v.x, v.z);
    }
    public static Vector3 xxw(this Vector4 v) {
        return new Vector3(v.x, v.x, v.w);
    }
    public static Vector3 xyx(this Vector4 v) {
        return new Vector3(v.x, v.y, v.x);
    }
    public static Vector3 xzx(this Vector4 v) {
        return new Vector3(v.x, v.z, v.x);
    }
    public static Vector3 xwx(this Vector4 v) {
        return new Vector3(v.x, v.w, v.x);
    }
    public static Vector3 xyy(this Vector4 v) {
        return new Vector3(v.x, v.y, v.y);
    }
    public static Vector3 xyz(this Vector4 v) {
        return new Vector3(v.x, v.y, v.z);
    }
    public static Vector3 xyw(this Vector4 v) {
        return new Vector3(v.x, v.y, v.w);
    }
    public static Vector3 xzy(this Vector4 v) {
        return new Vector3(v.x, v.z, v.y);
    }
    public static Vector3 xwy(this Vector4 v) {
        return new Vector3(v.x, v.w, v.y);
    }
    public static Vector3 xzz(this Vector4 v) {
        return new Vector3(v.x, v.z, v.z);
    }
    public static Vector3 xzw(this Vector4 v) {
        return new Vector3(v.x, v.z, v.w);
    }

    public static Vector3 yxx(this Vector4 v) {
        return new Vector3(v.y, v.x, v.x);
    }
    public static Vector3 yxy(this Vector4 v) {
        return new Vector3(v.x, v.x, v.y);
    }
    public static Vector3 yxz(this Vector4 v) {
        return new Vector3(v.y, v.x, v.z);
    }
    public static Vector3 yyx(this Vector4 v) {
        return new Vector3(v.y, v.y, v.x);
    }
    public static Vector3 yzx(this Vector4 v) {
        return new Vector3(v.y, v.z, v.x);
    }
    public static Vector3 yyy(this Vector4 v) {
        return new Vector3(v.y, v.y, v.y);
    }
    public static Vector3 yyz(this Vector4 v) {
        return new Vector3(v.y, v.y, v.z);
    }
    public static Vector3 yzy(this Vector4 v) {
        return new Vector3(v.y, v.z, v.y);
    }
    public static Vector3 yzz(this Vector4 v) {
        return new Vector3(v.y, v.z, v.z);
    }
    public static Vector3 zxx(this Vector4 v) {
        return new Vector3(v.z, v.x, v.x);
    }
    public static Vector3 zxy(this Vector4 v) {
        return new Vector3(v.z, v.x, v.y);
    }
    public static Vector3 zxz(this Vector4 v) {
        return new Vector3(v.z, v.x, v.z);
    }
    public static Vector3 zyx(this Vector4 v) {
        return new Vector3(v.z, v.y, v.x);
    }
    public static Vector3 zzx(this Vector4 v) {
        return new Vector3(v.z, v.z, v.x);
    }
    public static Vector3 zyy(this Vector4 v) {
        return new Vector3(v.z, v.y, v.y);
    }
    public static Vector3 zyz(this Vector4 v) {
        return new Vector3(v.z, v.y, v.z);
    }
    public static Vector3 zzy(this Vector4 v) {
        return new Vector3(v.z, v.z, v.y);
    }
    public static Vector3 zzz(this Vector4 v) {
        return new Vector3(v.z, v.z, v.z);
    }

    #endregion
    #endregion
    #endregion
}
