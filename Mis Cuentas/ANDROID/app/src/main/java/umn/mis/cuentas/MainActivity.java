package umn.mis.cuentas;

import android.app.AlertDialog;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.SharedPreferences;
import android.net.ConnectivityManager;
import android.net.NetworkInfo;
import android.os.AsyncTask;
import android.os.Bundle;
import android.os.Handler;
import android.os.Message;
import android.support.design.widget.FloatingActionButton;
import android.support.design.widget.Snackbar;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.Toolbar;
import android.util.Log;
import android.view.View;
import android.view.Menu;
import android.view.MenuItem;
import android.widget.ArrayAdapter;
import android.widget.ListView;
import android.widget.Toast;

import com.google.android.gms.gcm.GoogleCloudMessaging;

import org.json.JSONArray;
import org.json.JSONObject;

import java.io.BufferedInputStream;
import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.URL;
import java.util.ArrayList;

public class MainActivity extends AppCompatActivity {
    public ArrayList<String> lista;
   public ArrayAdapter<String> adaptor;
    public String PROJECT_NUMBER = "680915653090";
    GoogleCloudMessaging gcm;
    String regid;
    public MainActivity reference;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        Toolbar toolbar = (Toolbar) findViewById(R.id.toolbar);
        setSupportActionBar(toolbar);
        reference = this;

        lista = new ArrayList<String>();
        lista.add("Mi Presupuesto");
        lista.add("Solicitud a contabilidad");
        lista.add("Informe de Gastos");
        SharedPreferences.Editor editor = getSharedPreferences("miscuentas", MODE_PRIVATE).edit();

        SharedPreferences prefs = getSharedPreferences("miscuentas", MODE_PRIVATE);
        int primeraVez = prefs.getInt("primeraVez", 0);
        if(primeraVez==0)
        {
            editor.putInt("primeraVez",1);
            getRegId();
        }
        


        editor.putString("WHO", "ERALOFE01");
        editor.commit();


        adaptor = new ArrayAdapter<String>(this,  android.R.layout.simple_list_item_1, lista);
        ListView listaView = (ListView)findViewById(R.id.menuListView);
        listaView.setAdapter(adaptor);


        FloatingActionButton fab = (FloatingActionButton) findViewById(R.id.fab);
        fab.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Snackbar.make(view, "Replace with your own action", Snackbar.LENGTH_LONG)
                        .setAction("Action", null).show();
            }
        });
    }
    public void getRegId(){
        new AsyncTask<Void, Void, String>() {
            @Override
            protected String doInBackground(Void... params) {
                String msg = "";
                try {
                    if (gcm == null) {
                        gcm = GoogleCloudMessaging.getInstance(getApplicationContext());
                    }
                    regid = gcm.register(PROJECT_NUMBER);
                    msg = "Device registered, registration ID=" + regid;
                    Log.i("GCM", msg);

                } catch (IOException ex) {
                    msg = "Error :" + ex.getMessage();

                }
                return msg;
            }

            @Override
            protected void onPostExecute(String msg) {
                if(isNetworkAvailable())
                {
                    try {
                         hazPost(regid);
                    }
                    catch (Exception e)
                    {

                    }

                } else {
                    new AlertDialog.Builder(reference)
                            .setTitle("Internet error")
                            .setMessage("No esta disponible una conexión a internet, por favor, conectese a internet para continuar.")
                            .setPositiveButton(android.R.string.ok, new DialogInterface.OnClickListener() {
                                public void onClick(DialogInterface dialog, int which) {
                                    // continue with delete
                                }
                            })
                            .setIcon(android.R.drawable.ic_dialog_alert)
                            .show();
                }

                //  etRegId.setText(msg + "\n");
            }
        }.execute(null, null, null);
    }

    private void hazPost(final String argumento2)
    {
        Toast.makeText(getBaseContext(),
                "Please wait, connecting to server.",
                Toast.LENGTH_SHORT).show();


        // Create Inner Thread Class
        Thread background = new Thread(new Runnable() {


            // After call for background.start this run method call
            public void run() {
                try {
                    SharedPreferences prefs = getSharedPreferences("miscuentas", MODE_PRIVATE);
                    String restoredText = prefs.getString("WHO", null);
                    String WHO="";
                    if (restoredText != null) {
                        WHO = prefs.getString("WHO", "No name defined");//"No name defined" is the default value.
                   }
                    String  urlString = new String("http://sunplus.redirectme.net:90/?accion=4&argumento1="+WHO+"&argumento2="+argumento2+"&argumento3=2");
                   Log.i("GCM",urlString);
                    URL url = new URL(urlString);
                    HttpURLConnection urlConnection = (HttpURLConnection) url.openConnection();

                    try {
                        InputStream in = new BufferedInputStream(urlConnection.getInputStream());
                        // Acciones a realizar con el flujo de datos
                        BufferedReader streamReader = new BufferedReader(new InputStreamReader(in, "UTF-8"));
                        StringBuilder responseStrBuilder = new StringBuilder();

                        String inputStr;
                        while ((inputStr = streamReader.readLine()) != null)
                            responseStrBuilder.append(inputStr);
                        JSONObject test = new JSONObject(responseStrBuilder.toString());

                        String success = test.getString("success");
                        if(success.equals("1"))
                        {
                            int gato = 5;
                        }

                     /*   reference.runOnUiThread(new Runnable() {
                            public void run() {

                            }
                        });
*/


                    }
                    catch (Exception e)
                    {
                        Log.i("Animation", e.toString());

                    }
                    finally {
                        urlConnection.disconnect();
                    }


                } catch (Throwable t) {
                    // just end the background thread
                    Log.i("Animation", "Thread  exception " + t);
                }
            }

            private void threadMsg(String msg) {

                if (!msg.equals(null) && !msg.equals("")) {
                    Message msgObj = handler.obtainMessage();
                    Bundle b = new Bundle();
                    b.putString("message", msg);
                    msgObj.setData(b);
                    handler.sendMessage(msgObj);
                }
            }

            // Define the Handler that receives messages from the thread and update the progress
            private final Handler handler = new Handler() {

                public void handleMessage(Message msg) {

                    String aResponse = msg.getData().getString("message");

                    if ((null != aResponse)) {

                        // ALERT MESSAGE
                        Toast.makeText(
                                getBaseContext(),
                                "Server Response: "+aResponse,
                                Toast.LENGTH_SHORT).show();
                    }
                    else
                    {

                        // ALERT MESSAGE
                        Toast.makeText(
                                getBaseContext(),
                                "Not Got Response From Server.",
                                Toast.LENGTH_SHORT).show();
                    }

                }
            };

        });
        // Start Thread
        background.start();
    }

    private boolean isNetworkAvailable() {
        ConnectivityManager connectivityManager
                = (ConnectivityManager) getSystemService(Context.CONNECTIVITY_SERVICE);
        NetworkInfo activeNetworkInfo = connectivityManager.getActiveNetworkInfo();
        //return activeNetworkInfo != null && activeNetworkInfo.isConnected();
        return activeNetworkInfo != null && activeNetworkInfo.isConnectedOrConnecting();
    }


    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu; this adds items to the action bar if it is present.
        getMenuInflater().inflate(R.menu.menu_main, menu);
        return true;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        // Handle action bar item clicks here. The action bar will
        // automatically handle clicks on the Home/Up button, so long
        // as you specify a parent activity in AndroidManifest.xml.
        int id = item.getItemId();

        //noinspection SimplifiableIfStatement
        if (id == R.id.action_settings) {
            return true;
        }

        return super.onOptionsItemSelected(item);
    }

}
