using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AnimatorTriggerBool{
    bool value;
    Animator animator;
    string name;
    public AnimatorTriggerBool(Animator animator, string name, bool init = false){
        this.animator = animator;
        this.name = name;
        this.value = init;
    }
    public static AnimatorTriggerBool operator +(AnimatorTriggerBool a, bool b){
        a.value = b;
        return a;
    }
    public bool Get(){
        return value;
    }
    public void Set(bool input){
        value = input;
        if (animator != null && animator.parameters.Any(a => a.name == this.name)){
            animator.SetBool(name, value);
        }
    }
    public static implicit operator bool(AnimatorTriggerBool b){
        return b.value;
    }
}

public class Tween{
    public static float Pow(float current, float min, float max, int power = 5){
        float currentRate = (current - min) / (max - min);
        float result = 1-Mathf.Pow(1-currentRate, power);
        return result;
    }
}