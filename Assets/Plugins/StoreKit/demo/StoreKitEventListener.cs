using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class StoreKitEventListener : MonoBehaviour
{
//#if UNITY_IPHONE
    private int Vopr = 0;
    private int Gold = 0;

    private string VoprStr = "";
    private string GoldStr = "";

	void OnEnable()
	{
		// Listens to all the StoreKit events.  All event listeners MUST be removed before this object is disposed!
		StoreKitManager.productPurchaseAwaitingConfirmationEvent += productPurchaseAwaitingConfirmationEvent;
		StoreKitManager.purchaseSuccessfulEvent += purchaseSuccessful;
		StoreKitManager.purchaseCancelledEvent += purchaseCancelled;
		StoreKitManager.purchaseFailedEvent += purchaseFailed;
		StoreKitManager.productListReceivedEvent += productListReceivedEvent;
		StoreKitManager.productListRequestFailedEvent += productListRequestFailed;
		StoreKitManager.restoreTransactionsFailedEvent += restoreTransactionsFailed;
		StoreKitManager.restoreTransactionsFinishedEvent += restoreTransactionsFinished;
		StoreKitManager.paymentQueueUpdatedDownloadsEvent += paymentQueueUpdatedDownloadsEvent;
	}
	
	
	void OnDisable()
	{
		// Remove all the event handlers
		StoreKitManager.productPurchaseAwaitingConfirmationEvent -= productPurchaseAwaitingConfirmationEvent;
		StoreKitManager.purchaseSuccessfulEvent -= purchaseSuccessful;
		StoreKitManager.purchaseCancelledEvent -= purchaseCancelled;
		StoreKitManager.purchaseFailedEvent -= purchaseFailed;
		StoreKitManager.productListReceivedEvent -= productListReceivedEvent;
		StoreKitManager.productListRequestFailedEvent -= productListRequestFailed;
		StoreKitManager.restoreTransactionsFailedEvent -= restoreTransactionsFailed;
		StoreKitManager.restoreTransactionsFinishedEvent -= restoreTransactionsFinished;
		StoreKitManager.paymentQueueUpdatedDownloadsEvent -= paymentQueueUpdatedDownloadsEvent;
	}
	
	
	void productListReceivedEvent( List<StoreKitProduct> productList )
	{
		Debug.Log( "productListReceivedEvent. total products received: " + productList.Count );
		
		// print the products to the console
		foreach( StoreKitProduct product in productList )
			Debug.Log( product.ToString() + "\n" );
	}
	
	
	void productListRequestFailed( string error )
	{
		Debug.Log( "productListRequestFailed: " + error );
	}
	

	void purchaseFailed( string error )
	{
		Debug.Log( "purchase failed with error: " + error );
	}
	

	void purchaseCancelled( string error )
	{
		Debug.Log( "purchase cancelled with error: " + error );
	}
	
	
	void productPurchaseAwaitingConfirmationEvent( StoreKitTransaction transaction )
	{
		Debug.Log( "productPurchaseAwaitingConfirmationEvent: " + transaction );
	}
	
	
	void purchaseSuccessful( StoreKitTransaction transaction )
	{
		Debug.Log( "purchased product: " + transaction );
        if (GetComponent<StoreKitGUIManager>().ActivePurchase == 1)
        {
            Vopr += 1;
            VoprStr = transaction.ToString();
        }

        if (GetComponent<StoreKitGUIManager>().ActivePurchase == 2)
        {
            Gold += 1;
            GoldStr = transaction.ToString();
        }
	}
	
	
	void restoreTransactionsFailed( string error )
	{
		Debug.Log( "restoreTransactionsFailed: " + error );
	}
	
	
	void restoreTransactionsFinished()
	{
		Debug.Log( "restoreTransactionsFinished" );
	}
	
	
	void paymentQueueUpdatedDownloadsEvent( List<StoreKitDownload> downloads )
	{
		Debug.Log( "paymentQueueUpdatedDownloadsEvent: " );
		foreach( var dl in downloads )
			Debug.Log( dl );
	}

    private void OnGUI()
    {
        GUI.Label(new Rect(20, Screen.height - 80,300,30), "50 questions: " + Vopr.ToString());
        GUI.Label(new Rect(20, Screen.height - 60, 300, 30), "Gold: " + Gold.ToString());

        if (Vopr > 0) 
            GUI.Label(new Rect(20, Screen.height - 40, 300, 30), "purchased product: " + VoprStr);
        if (Gold > 0) 
            GUI.Label(new Rect(20, Screen.height - 20, 300, 30), "purchased product: " + GoldStr);
    }
	
//#endif
}

