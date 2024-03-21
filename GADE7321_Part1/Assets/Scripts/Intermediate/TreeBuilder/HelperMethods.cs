using UnityEngine;

namespace Utils
{
    public static class HelperMethods
    {
        public static bool IsDistanceLessThan(Transform entityA, Transform entityB, float distance)
        {
            float calcDistance = Vector3.Distance(entityA.position, entityB.position);
            return calcDistance <= distance;
        }

        public static bool IsCarryFlag(Transform entity)
        {
            if(entity.TryGetComponent<FlagComponent>(out FlagComponent flag))
            {
                return flag.isHolding;
            }

            return false;
        }
    }
}