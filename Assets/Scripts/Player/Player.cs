﻿using System;
using Enemies;
using SpellSystem;
using UnityEngine;

namespace Player
{
    public class Player : MonoBehaviour, IAttackEnemyTarget
    {
        [SerializeField] private CharacterController characterController;
        [SerializeField] private HealthSystem.HealthSystem healthSystem;
        [SerializeField] private SpellBase[] spells;
        [SerializeField] private float speed = 2f;
        [SerializeField] private float radiusForAttack = 0.5f;

        private PlayerInputController _playerInputController;
        private PlayerMovement _playerMovement;

        private SpellController _spellController;
        
        public event Action Dead;

        public Vector3 Position => transform.position;
        public float RadiusForAttack => radiusForAttack;

        private void Awake()
        {
            _playerInputController = new PlayerInputController();
            _playerMovement = new PlayerMovement(characterController, _playerInputController, speed);

            _spellController = new SpellController(spells, _playerInputController, transform);
        }

        private void OnEnable() => healthSystem.Dead += HealthSystemDeadHandler;
        private void OnDisable() => healthSystem.Dead -= HealthSystemDeadHandler;

        private void Update() => _playerMovement.Tick();
        
        private void HealthSystemDeadHandler() => Dead?.Invoke();

        public void ApplyDamage(float damage) => healthSystem.ApplyDamage(damage);
    }
}