using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
[CreateAssetMenu(fileName = "InteractionData", menuName = "Data/InteractionData", order = int.MaxValue)]
public class InteractionData : ScriptableObject
{
    [SerializeField]private List<InteractionInfo> interactionInfo;
    public List<InteractionInfo> InteractionInfo { get { return interactionInfo;} }

}
[System.Serializable]
public class InteractionInfo
{
    public string name;
    public string description;
}