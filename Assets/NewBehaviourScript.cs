using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class EnterLoadScene : MonoBehaviour
{
    [Tooltip("اسم المشهد اللي عايز تحميله (أو تقدر تدخل رقم الفهرس)")]
    public string sceneName = "NextScene";

    [Tooltip("لو حضرتك تفضل تستخدم فهرس المشهد بدل الاسم، ضع index >= 0 واخلِ sceneName فارغ")]
    public int sceneBuildIndex = -1;

    [Tooltip("لازم الـ GameObject اللي يمثل اللاعب يكون عليه هذا الـ Tag (افتراضي: Player)")]
    public string playerTag = "Player";

    [Tooltip("تأخير قبل التحميل (ثواني) — لو عايز تحميل مباشر خليها 0")]
    public float delaySeconds = 0f;

    [Tooltip("لو true هيستخدم تحميل غير متزامن (مناسب لتحميل خلفي)")]
    public bool loadAsync = true;

    [Tooltip("لو true سيمنع إعادة التحميل لو تم الدخول مره تانية")]
    public bool oneTimeOnly = true;

    bool hasTriggered = false;

    void Reset()
    {
        // افتراضيًا خليه trigger
        Collider c = GetComponent<Collider>();
        if (c != null) c.isTrigger = true;
    }

    // يفضل استخدام Trigger لتحريك اللاعب عبره
    private void OnTriggerEnter(Collider other)
    {
        if (hasTriggered && oneTimeOnly) return;
        if (other.CompareTag(playerTag))
        {
            hasTriggered = true;
            if (delaySeconds > 0f)
                StartCoroutine(LoadAfterDelay(delaySeconds));
            else
                StartCoroutine(LoadSceneCoroutine());
        }
    }

    private IEnumerator LoadAfterDelay(float d)
    {
        yield return new WaitForSeconds(d);
        yield return LoadSceneCoroutine();
    }

    private IEnumerator LoadSceneCoroutine()
    {
        if (sceneBuildIndex >= 0)
        {
            if (loadAsync)
            {
                AsyncOperation op = SceneManager.LoadSceneAsync(sceneBuildIndex);
                while (!op.isDone) yield return null;
            }
            else
            {
                SceneManager.LoadScene(sceneBuildIndex);
                yield break;
            }
        }
        else if (!string.IsNullOrEmpty(sceneName))
        {
            if (loadAsync)
            {
                AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);
                while (!op.isDone) yield return null;
            }
            else
            {
                SceneManager.LoadScene(sceneName);
                yield break;
            }
        }
        else
        {
            Debug.LogWarning("EnterLoadScene: no sceneName set and sceneBuildIndex < 0");
            yield break;
        }
    }
}
