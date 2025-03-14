using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class GetTargetFromMouse : MonoBehaviour
{
    public GameObject GetTarget()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit[] hits = Physics.RaycastAll(ray);

        /// Filtrar los objetos que implementan ISelectable y ordenar por distancia
        var closestSelectable = hits
            .Select(hit => new { hit, selectable = hit.collider.GetComponent<ISelectable>() })
            .Where(item => item.selectable != null)
            .OrderBy(item => item.hit.distance)
            .FirstOrDefault(); // Tomar el más cercano

        if (closestSelectable != null)
        {
            GameObject selectedGO = closestSelectable.selectable.GetGO();
            Debug.Log("Objeto seleccionado: " + selectedGO.name);
            return selectedGO;
        }

        return null;
    }
    
}