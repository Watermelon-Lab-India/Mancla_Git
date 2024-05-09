using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Purchasing;
using UnityEngine.UI;
public class PurchaserLogic : MonoBehaviour, IStoreListener
{
	private static IStoreController storeController;
	private static IExtensionProvider extensionProvider;

	public Text logicTest;
    public GameObject loadingPanel;

    #region
#if UNITY_ANDROID && !UNITY_EDITOR
    public const string productId1 = "coin_package1";
	public const string productId2 = "coin_package2";
	public const string productId3 = "coin_package3";
	public const string productId4 = "coin_package4";
	public const string productId5 = "remove_ads";
	public const string productId6 = "table_5";
	public const string productId7 = "table_6";
#else
    public const string productId1 = "coinpack_1";
    public const string productId2 = "coinpack_2";
    public const string productId3 = "coin_package3";
    public const string productId4 = "coin_package4";
    public const string productId5 = "remove_ads";
    public const string productId6 = "table_5";
    public const string productId7 = "table_6";
#endif
    #endregion


    void Start()
	{
        InitializePurchasing();
    }

	private bool IsInitialized()
	{
		return (storeController != null && extensionProvider != null);
	}

	public void InitializePurchasing()
	{
        
        if (IsInitialized())
			return;

		var module = StandardPurchasingModule.Instance();

		ConfigurationBuilder builder = ConfigurationBuilder.Instance(module);
        //builder.AddProduct(productId1, ProductType.Consumable);
        //builder.AddProduct(productId2, ProductType.Consumable);
        //builder.AddProduct(productId3, ProductType.Consumable);
        //builder.AddProduct(productId4, ProductType.Consumable);
        //builder.AddProduct(productId5, ProductType.NonConsumable);
        //builder.AddProduct(productId6, ProductType.NonConsumable);
        //builder.AddProduct(productId7, ProductType.NonConsumable);

#if UNITY_ANDROID && !UNITY_EDITOR
    builder.AddProduct(productId1, ProductType.Consumable, new IDs
			{
				//            { productId1, AppleAppStore.Name },
				{ productId1, GooglePlay.Name },
			});

		builder.AddProduct(productId2, ProductType.Consumable, new IDs
			{
				//            { productId2, AppleAppStore.Name },
				{ productId2, GooglePlay.Name }, }
		);

		builder.AddProduct(productId3, ProductType.Consumable, new IDs
			{
				//            { productId3, AppleAppStore.Name },
				{ productId3, GooglePlay.Name },
			});

		builder.AddProduct(productId4, ProductType.Consumable, new IDs
			{
				//            { productId4, AppleAppStore.Name },
				{ productId4, GooglePlay.Name },
			});

		builder.AddProduct(productId5, ProductType.NonConsumable, new IDs
			{
				//            { productId5, AppleAppStore.Name },
				{ productId5, GooglePlay.Name },
			});

		builder.AddProduct(productId6, ProductType.NonConsumable, new IDs
			{
				//            { productId6, AppleAppStore.Name },
				{ productId6, GooglePlay.Name },
			});

		builder.AddProduct(productId7, ProductType.NonConsumable, new IDs
			{
				//            { productId7, AppleAppStore.Name },
				{ productId7, GooglePlay.Name },
			});
#else
        builder.AddProduct(productId1, ProductType.Consumable, new IDs
            {
                { productId1, AppleAppStore.Name },

            });

        builder.AddProduct(productId2, ProductType.Consumable, new IDs
            {
                { productId2, AppleAppStore.Name },

            });

        builder.AddProduct(productId3, ProductType.Consumable, new IDs
            {
                 { productId3, AppleAppStore.Name },

            });

        builder.AddProduct(productId4, ProductType.Consumable, new IDs
            {
                { productId4, AppleAppStore.Name },

            });

        builder.AddProduct(productId5, ProductType.NonConsumable, new IDs
            {
                 { productId5, AppleAppStore.Name },

            });

        builder.AddProduct(productId6, ProductType.NonConsumable, new IDs
            {
                { productId6, AppleAppStore.Name },

            });

        builder.AddProduct(productId7, ProductType.NonConsumable, new IDs
            {
               { productId7, AppleAppStore.Name },

            });
#endif
        UnityPurchasing.Initialize(this, builder);
	}

	public void BuyProductID(string productId)
	{
        if (productId == "1")
        {
            productId = productId1;
        }
        else if (productId == "2")
        {
            productId = productId2;
        }
        

        logicTest.text += productId;
        if(!loadingPanel.activeSelf)
        {
		try
		{
			if (IsInitialized())
			{
                if(loadingPanel.activeSelf)
                {
                    loadingPanel.SetActive(false);
                }
				//logicTest.text += "  --- initialized -----"; 
				Product p = storeController.products.WithID(productId);
				//logicTest.text += " --- store controller 11111----" + p.availableToPurchase;
				if (p != null && p.availableToPurchase)
				{
				//	logicTest.text += "  --- successed -----" + p.definition.id;
					Debug.Log(string.Format("Purchasing product asychronously: '{0}'", p.definition.id));
					storeController.InitiatePurchase(p);
				}
				else
				{
					//logicTest.text += "  --- failed -----";
					Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
				}
			}
			else
			{
                if(!loadingPanel.activeSelf)
                {
                    loadingPanel.SetActive(true);
                }
			//	logicTest.text += "  BuyProductID FAIL. Not initialized.";
				Debug.Log("BuyProductID FAIL. Not initialized.");
			}
		}
		catch (Exception e)
		{
			//logicTest.text += "BuyProductID: FAIL. Exception during purchase. " + e;
			Debug.Log("BuyProductID: FAIL. Exception during purchase. " + e);
		}

        }
	}

	public void RestorePurchase()
	{
		if (!IsInitialized())
		{
			Debug.Log("RestorePurchases FAIL. Not initialized.");
			return;
		}

        if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.OSXPlayer)
        {
            Debug.Log("RestorePurchases started ...");

            var apple = extensionProvider.GetExtension<IAppleExtensions>();

            apple.RestoreTransactions
            (
                (result) => { Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore."); }
            );
        }
        else
        {
            Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
        }
    }

	public void OnInitialized(IStoreController sc, IExtensionProvider ep)
	{
		Debug.Log("OnInitialized : PASS");
		logicTest.text += "  OnInitialized : PASS   " + sc.products.all[0].metadata.localizedDescription;
		storeController = sc;
		extensionProvider = ep;

        #if UNITY_IOS
        if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.OSXPlayer)
        {
            Debug.Log("RestorePurchases started ...");

            var apple = extensionProvider.GetExtension<IAppleExtensions>();

            apple.RestoreTransactions
            (
                (result) => { Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore."); }
            );
        }
        else
        {
            Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
        }
        #endif
    }

	public void OnInitializeFailed(InitializationFailureReason reason)
	{
		logicTest.text += "  initialize failed";
		Debug.Log("OnInitializeFailed InitializationFailureReason:" + reason);
	}

	public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
	{
		Debug.Log("Receipt: " + args.purchasedProduct.receipt);
		Debug.Log("Receipt: " + args.purchasedProduct.definition.storeSpecificId);
		Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
		logicTest.text += "  --- completed -----";
		switch (args.purchasedProduct.definition.id)
		{
		case productId1://3000 coins
			Debug.Log("product 1");
			this.gameObject.GetComponent<UIManager>().BuyCoinsResult(1);
			break;
		case productId2://10000 coins
			Debug.Log("product 2");
			this.gameObject.GetComponent<UIManager>().BuyCoinsResult(2);
			break;
		case productId3://30000 coins
			Debug.Log("product 3");
			this.gameObject.GetComponent<UIManager>().BuyCoinsResult(3);
			break;
		case productId4://100000 coins
			this.gameObject.GetComponent<UIManager>().BuyCoinsResult(4);
			Debug.Log("product 4");
			break;
		case productId5:
			Debug.Log("product 5");
			this.gameObject.GetComponent<UIManager>().BuyCoinsResult(5);
			break;
		case productId6:
			Debug.Log("product 6");
			this.gameObject.GetComponent<UIManager>().BuyCoinsResult(6);
			break;
		case productId7:
			Debug.Log("product 7");
			this.gameObject.GetComponent<UIManager>().BuyCoinsResult(7);
			break;
		}
		return PurchaseProcessingResult.Complete;
	}

	public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
	{
		logicTest.text += "  --- !!!! purchase filed!!!!! -----" + failureReason.ToString();
		Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
	}
}

/*

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

// Placing the Purchaser class in the CompleteProject namespace allows it to interact with ScoreManager, 
// one of the existing Survival Shooter scripts.
namespace CompleteProject
    {
    // Deriving the Purchaser class from IStoreListener enables it to receive messages from Unity Purchasing.
    public class Purchaser : MonoBehaviour, IStoreListener
        {
        private static IStoreController m_StoreController;          // The Unity Purchasing system.
        private static IExtensionProvider m_StoreExtensionProvider; // The store-specific Purchasing subsystems.

        // Product identifiers for all products capable of being purchased: 
        // "convenience" general identifiers for use with Purchasing, and their store-specific identifier 
        // counterparts for use with and outside of Unity Purchasing. Define store-specific identifiers 
        // also on each platform's publisher dashboard (iTunes Connect, Google Play Developer Console, etc.)

        // General product identifiers for the consumable, non-consumable, and subscription products.
        // Use these handles in the code to reference which product to purchase. Also use these values 
        // when defining the Product Identifiers on the store. Except, for illustration purposes, the 
        // kProductIDSubscription - it has custom Apple and Google identifiers. We declare their store-
        // specific mapping to Unity Purchasing's AddProduct, below.
        public static string kProductIDConsumable = "consumable";
        public static string kProductIDNonConsumable = "nonconsumable";
        public static string kProductIDSubscription = "subscription";

        // Apple App Store-specific product identifier for the subscription product.
        private static string kProductNameAppleSubscription = "com.unity3d.subscription.new";

        // Google Play Store-specific product identifier subscription product.
        private static string kProductNameGooglePlaySubscription = "com.unity3d.subscription.original";

        void Start()
            {
            // If we haven't set up the Unity Purchasing reference
            if (m_StoreController == null)
                {
                // Begin to configure our connection to Purchasing
                InitializePurchasing();
                }
            }

        public void InitializePurchasing()
            {
            // If we have already connected to Purchasing ...
            if (IsInitialized())
                {
                // ... we are done here.
                return;
                }

            // Create a builder, first passing in a suite of Unity provided stores.
            var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

            // Add a product to sell / restore by way of its identifier, associating the general identifier
            // with its store-specific identifiers.
            builder.AddProduct(kProductIDConsumable, ProductType.Consumable);
            // Continue adding the non-consumable product.
            builder.AddProduct(kProductIDNonConsumable, ProductType.NonConsumable);
            // And finish adding the subscription product. Notice this uses store-specific IDs, illustrating
            // if the Product ID was configured differently between Apple and Google stores. Also note that
            // one uses the general kProductIDSubscription handle inside the game - the store-specific IDs 
            // must only be referenced here. 
            builder.AddProduct(kProductIDSubscription, ProductType.Subscription, new IDs(){
                { kProductNameAppleSubscription, AppleAppStore.Name },
                { kProductNameGooglePlaySubscription, GooglePlay.Name },
            });

            // Kick off the remainder of the set-up with an asynchrounous call, passing the configuration 
            // and this class' instance. Expect a response either in OnInitialized or OnInitializeFailed.
            UnityPurchasing.Initialize(this, builder);
            }


        private bool IsInitialized()
            {
            // Only say we are initialized if both the Purchasing references are set.
            return m_StoreController != null && m_StoreExtensionProvider != null;
            }


        public void BuyConsumable()
            {
            // Buy the consumable product using its general identifier. Expect a response either 
            // through ProcessPurchase or OnPurchaseFailed asynchronously.
            BuyProductID(kProductIDConsumable);
            }


        public void BuyNonConsumable()
            {
            // Buy the non-consumable product using its general identifier. Expect a response either 
            // through ProcessPurchase or OnPurchaseFailed asynchronously.
            BuyProductID(kProductIDNonConsumable);
            }


        public void BuySubscription()
            {
            // Buy the subscription product using its the general identifier. Expect a response either 
            // through ProcessPurchase or OnPurchaseFailed asynchronously.
            // Notice how we use the general product identifier in spite of this ID being mapped to
            // custom store-specific identifiers above.
            BuyProductID(kProductIDSubscription);
            }


        void BuyProductID(string productId)
            {
            // If Purchasing has been initialized ...
            if (IsInitialized())
                {
                // ... look up the Product reference with the general product identifier and the Purchasing 
                // system's products collection.
                Product product = m_StoreController.products.WithID(productId);

                // If the look up found a product for this device's store and that product is ready to be sold ... 
                if (product != null && product.availableToPurchase)
                    {
                    Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                    // ... buy the product. Expect a response either through ProcessPurchase or OnPurchaseFailed 
                    // asynchronously.
                    m_StoreController.InitiatePurchase(product);
                    }
                // Otherwise ...
                else
                    {
                    // ... report the product look-up failure situation  
                    Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
                    }
                }
            // Otherwise ...
            else
                {
                // ... report the fact Purchasing has not succeeded initializing yet. Consider waiting longer or 
                // retrying initiailization.
                Debug.Log("BuyProductID FAIL. Not initialized.");
                }
            }


        // Restore purchases previously made by this customer. Some platforms automatically restore purchases, like Google. 
        // Apple currently requires explicit purchase restoration for IAP, conditionally displaying a password prompt.
        public void RestorePurchases()
            {
            // If Purchasing has not yet been set up ...
            if (!IsInitialized())
                {
                // ... report the situation and stop restoring. Consider either waiting longer, or retrying initialization.
                Debug.Log("RestorePurchases FAIL. Not initialized.");
                return;
                }

            // If we are running on an Apple device ... 
            if (Application.platform == RuntimePlatform.IPhonePlayer ||
                Application.platform == RuntimePlatform.OSXPlayer)
                {
                // ... begin restoring purchases
                Debug.Log("RestorePurchases started ...");

                // Fetch the Apple store-specific subsystem.
                var apple = m_StoreExtensionProvider.GetExtension<IAppleExtensions>();
                // Begin the asynchronous process of restoring purchases. Expect a confirmation response in 
                // the Action<bool> below, and ProcessPurchase if there are previously purchased products to restore.
                apple.RestoreTransactions((result) => {
                    // The first phase of restoration. If no more responses are received on ProcessPurchase then 
                    // no purchases are available to be restored.
                    Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
                });
                }
            // Otherwise ...
            else
                {
                // We are not running on an Apple device. No work is necessary to restore purchases.
                Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
                }
            }


        //  
        // --- IStoreListener
        //

        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
            {
            // Purchasing has succeeded initializing. Collect our Purchasing references.
            Debug.Log("OnInitialized: PASS");

            // Overall Purchasing system, configured with products for this application.
            m_StoreController = controller;
            // Store specific subsystem, for accessing device-specific store features.
            m_StoreExtensionProvider = extensions;
            }


        public void OnInitializeFailed(InitializationFailureReason error)
            {
            // Purchasing set-up has not succeeded. Check error for reason. Consider sharing this reason with the user.
            Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
            }


        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
            {
            // A consumable product has been purchased by this user.
            if (String.Equals(args.purchasedProduct.definition.id, kProductIDConsumable, StringComparison.Ordinal))
                {
                Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
                // The consumable item has been successfully purchased, add 100 coins to the player's in-game score.
                ScoreManager.score += 100;
                }
            // Or ... a non-consumable product has been purchased by this user.
            else if (String.Equals(args.purchasedProduct.definition.id, kProductIDNonConsumable, StringComparison.Ordinal))
                {
                Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
                // TODO: The non-consumable item has been successfully purchased, grant this item to the player.
                }
            // Or ... a subscription product has been purchased by this user.
            else if (String.Equals(args.purchasedProduct.definition.id, kProductIDSubscription, StringComparison.Ordinal))
                {
                Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
                // TODO: The subscription item has been successfully purchased, grant this to the player.
                }
            // Or ... an unknown product has been purchased by this user. Fill in additional products here....
            else
                {
                Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
                }

            // Return a flag indicating whether this product has completely been received, or if the application needs 
            // to be reminded of this purchase at next app launch. Use PurchaseProcessingResult.Pending when still 
            // saving purchased products to the cloud, and when that save is delayed. 
            return PurchaseProcessingResult.Complete;
            }


        public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
            {
            // A product purchase attempt did not succeed. Check failureReason for more detail. Consider sharing 
            // this reason with the user to guide their troubleshooting actions.
            Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
            }
        }
    }
    */