using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

// ReSharper disable FieldCanBeMadeReadOnly.Local

namespace SampleGame {
    public sealed class CurrencyViewAnimated : MonoBehaviour {
        [SerializeField]
        private TMP_Text currencyText;

        [SerializeField]
        private float animationDuration = 1.0f;

        [SerializeField]
        private Color spendColor;

        [SerializeField]
        private Color earnColor;

        private Coroutine _animationCoroutine;
        private List<Sequence> _animationSequences = new();

        public void SetCurrency(string currency) {
            this.StopAnimations();
            this.currencyText.text = currency;
        }

        public void EarnCurrency(int startCurrency, int range, string format = "{0}") {
            this.StopAnimations();

            _animationCoroutine = this.StartCoroutine(this.AddCurrencyAnimation(startCurrency, range, format));
            this.BounceAnimation();
            this.ColorAnimation(this.earnColor, this.animationDuration - 0.3f);
        }

        public void SpendCurrency(string currency) {
            this.StopAnimations();

            this.currencyText.text = currency;

            this.BounceAnimation();
            this.ColorAnimation(this.spendColor);
        }

        private void ColorAnimation(Color color, float interval = 0.5f) {
            Sequence sequence = DOTween.Sequence();
            sequence
                .AppendCallback(() => _animationSequences.Add(sequence))
                .Append(this.currencyText.DOColor(color, 0.1f))
                .AppendInterval(interval)
                .Append(this.currencyText.DOColor(Color.black, 0.3f))
                .OnComplete(() => _animationSequences.Remove(sequence));
        }

        private void BounceAnimation() {
            Sequence sequence = DOTween.Sequence();
            sequence
                .AppendCallback(() => _animationSequences.Add(sequence))
                .Append(this.currencyText.transform.DOScale(new Vector3(1.1f, 1.1f, 1.0f), 0.2f))
                .Append(this.currencyText.transform.DOScale(new Vector3(1.0f, 1.0f, 1.0f), 0.4f))
                .OnComplete(() => _animationSequences.Remove(sequence));
        }

        private void StopAnimations() {
            if (_animationCoroutine != null) {
                this.StopCoroutine(_animationCoroutine);
                _animationCoroutine = null;
            }

            foreach (var sequence in _animationSequences) {
                sequence.Kill();
            }

            _animationSequences.Clear();
        }

        private IEnumerator AddCurrencyAnimation(int startCurrency, int range, string format) {
            float progress = 0;

            while (progress <= 1) {
                yield return null;
                progress = Mathf.Min(1, progress + Time.deltaTime / this.animationDuration);

                int currentCurrency = Mathf.RoundToInt(startCurrency + range * progress);
                this.currencyText.text = string.Format(format, currentCurrency);
            }

            this.currencyText.text = string.Format(format, startCurrency + range);
        }
    }
}