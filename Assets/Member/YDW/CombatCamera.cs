using Core;
using DG.Tweening;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Member.YDW
{
    public class CombatCamera : MonoSingleton<CombatCamera>
    {
        [SerializeField] private Transform followPos;
        [SerializeField] private Transform centerPos;
        [SerializeField] private float defaultLensSize;
        private CinemachineCamera _camera;
        private CinemachineFollow _follow;

        
        protected override void Awake()
        {
            base.Awake();
            _camera = GetComponent<CinemachineCamera>();
            _follow = GetComponent<CinemachineFollow>();
        }

        private void Update()
        {
            if (Keyboard.current.zKey.wasPressedThisFrame)
                SetLensSize(3);
            if (Keyboard.current.oKey.wasPressedThisFrame)
                SetDefaultLensSize();
        }

        public void SetFollow(Transform target)
        { 
            _camera.Target.TrackingTarget = target;
        }

        public void MoveCenterPos()
        {
            followPos.position = centerPos.position;
            _camera.Target.TrackingTarget = followPos;
        }

        public void SetLensSize(float lensSize)
        {
            DOTween.To(() => _camera.Lens.OrthographicSize, x => _camera.Lens.OrthographicSize = x, lensSize, 1f)
                .SetEase(Ease.InOutSine);
        }

        public void SetDefaultLensSize()
        {
            DOTween.To(() => _camera.Lens.OrthographicSize, x => _camera.Lens.OrthographicSize = x, defaultLensSize, 1f)
                .SetEase(Ease.InOutSine);
        }

        public void SetFollowOffSet(Vector3 offset = default)
        {
            if (offset == default)
                offset = new Vector3(0, 0, -10);
            DOTween.To(() => _follow.FollowOffset.x, x =>  _follow.FollowOffset.x = x, offset.x, 1f)
                .SetEase(Ease.InOutSine);
            DOTween.To(() => _follow.FollowOffset.y, y =>  _follow.FollowOffset.y = y, offset.y, 1f)
                .SetEase(Ease.InOutSine);
        }





    }
}
