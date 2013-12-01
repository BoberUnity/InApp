using System;using UnityEngine;using System.Collections.Generic;using Prime31;public class StoreKitGUIManager : MonoBehaviourGUI{//#if UNITY_IPHONE	private List<StoreKitProduct> _products;    //private int avialableBuing = -1;    //private int canMake = -1;    //private int    //private string buyError = "";    private int activePurchase = 0; //50 - 1; gold - 100/    public int ActivePurchase    {        get { return activePurchase; }    }    //temp
    private string transact = "";
    public string Transact
    {
        get { return transact; }
    }    public event Action ErrorPurchase;
    public event Action NotPurchase;	void Start()	{		// you cannot make any purchases until you have retrieved the products from the server with the requestProductData method		// we will store the products locally so that we will know what is purchaseable and when we can purchase the products		StoreKitManager.productListReceivedEvent += allProducts =>		{			Debug.Log( "received total products: " + allProducts.Count );			_products = allProducts;		    //avialableBuing = allProducts.Count;		};        if (StoreKitBinding.canMakePayments())        {            var productIdentifiers = new string[] { "com.zuev.pdd.50", "com.zuev.pdd.gold" };            StoreKitBinding.requestProductData(productIdentifiers);        }	}	/*void OnGUI()	{		beginColumn();				if( GUILayout.Button( "Get Can Make Payments" ) )		{			bool canMakePayments = StoreKitBinding.canMakePayments();			Debug.Log( "StoreKit canMakePayments: " + canMakePayments );            if (canMakePayments)                canMake = 1;            else                canMake = 0;            		}						if( GUILayout.Button( "Get Product Data" ) )		{			// array of product ID's from iTunesConnect.  MUST match exactly what you have there!			var productIdentifiers = new string[] { "com.zuev.pdd.50", "com.zuev.pdd.gold"};			StoreKitBinding.requestProductData( productIdentifiers );		}						if( GUILayout.Button( "Restore Completed Transactions" ) )		{			StoreKitBinding.restoreCompletedTransactions();		}			endColumn( true );						// enforce the fact that we can't purchase products until we retrieve the product data		if( _products != null && _products.Count > 0 )		{			if( GUILayout.Button( "Purchase Random Product" ) )			{				var productIndex = Random.Range( 0, _products.Count );				var product = _products[productIndex];								Debug.Log( "preparing to purchase product: " + product.productIdentifier );				StoreKitBinding.purchaseProduct( product.productIdentifier, 1 );			}		}		else		{			GUILayout.Label( "Once you successfully request product data a purchase button will appear here for testing" );		}				if( GUILayout.Button( "Get Saved Transactions" ))		{			List<StoreKitTransaction> transactionList = StoreKitBinding.getAllSavedTransactions();						// Print all the transactions to the console			Debug.Log( "\ntotal transaction received: " + transactionList.Count );						foreach( StoreKitTransaction transaction in transactionList )			{			    //if (transaction ==                 //    )                Debug.Log( transaction.ToString() + "\n" );			}		}				if( GUILayout.Button( "Turn Off Auto Confirmation of Transactions" ) )		{			// this is used when you want to validate receipts on your own server or when dealing with iTunes hosted content			// you must manually call StoreKitBinding.finishPendingTransactions when the download/validation is complete			StoreKitManager.autoConfirmTransactions = false;		}						if( GUILayout.Button( "Cancel Downloads" ) )		{			StoreKitBinding.cancelDownloads();		}		if( GUILayout.Button( "Display App Store" ) )		{			StoreKitBinding.displayStoreWithProductId( "768171059" );//305967442		}				endColumn();	    GUI.Label(new Rect(20, Screen.height - 300, 300, 30), "Available buying: " + avialableBuing);        GUI.Label(new Rect(20, Screen.height - 280, 300, 30), "canMake: " + canMake);        if( _products != null && _products.Count > 0 )		{			if( GUI.Button(new Rect(20, Screen.height - 220, 150,30),  "Purchase 50" ) )			{				var product = _products[0];//50				Debug.Log( "preparing to purchase product: " + product.productIdentifier );				StoreKitBinding.purchaseProduct( product.productIdentifier, 1 );			    //buyError = "Buy 50 Sucessfull";			    activePurchase = 1;			}		}		        if( _products != null && _products.Count > 0 )		{			if( GUI.Button(new Rect(20, Screen.height - 160, 150,30),  "Purchase Gold" ) )			{				var product = _products[1];//50				Debug.Log( "preparing to purchase product: " + product.productIdentifier );				StoreKitBinding.purchaseProduct( product.productIdentifier, 1 );                //buyError = "Buy Gold Sucessfull";                activePurchase = 2;			}		}		        //GUI.Label(new Rect(20, Screen.height - 120, 300, 30), buyError);        	}*///#endif    public void Buy(int id)    {        if (_products != null && _products.Count > 0)        {           var product = _products[id];//0 - 50; 1 - gold           Debug.Log("preparing to purchase product: " + product.productIdentifier);           StoreKitBinding.purchaseProduct(product.productIdentifier, 1);           activePurchase = id;        }        else        {            var handler = ErrorPurchase;//ShowMessageWindow            if (handler != null)                handler();        }    }    public void Restore()    {        StoreKitBinding.restoreCompletedTransactions();        List<StoreKitTransaction> transactionList = StoreKitBinding.getAllSavedTransactions();        if (transactionList.Count == 0)
        {
            var handler = NotPurchase;//ShowMessageWindow
            if (handler != null)
                handler();
        }        Debug.Log("\ntotal transaction received: " + transactionList.Count);        foreach (StoreKitTransaction transaction in transactionList)        {            //if (transaction)            Debug.Log(transaction.ToString() + "\n");
            transact = transaction.ToString();
        }    }}