using Listen.Data;

using System.Diagnostics.Metrics;

namespace Listen
{
    public partial class MainForm : Form
    {

        public bool Active { get; set; } = false;
        private List<Listener> activeClients = new List<Listener>();
        private List<Thread> activeThreads = new List<Thread>();
        private Dictionary<string, ListViewItem> listenerListViewMap = new Dictionary<string, ListViewItem>();

        private string APIUrl;
        public MainForm()
        {
            InitializeComponent();
            ListenersListView.View = View.Details;

        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            FlipButtonState(ref StartButton);
            FlipButtonState(ref StopButton);

            FlipButtonState(ref RegisterButton);
            FlipButtonState(ref UnregisterButton);
            Active = true;

            APIUrl = BaseUrlTextBox.Text;
            BaseUrlTextBox.Enabled = false;

            Listener apiClient = new Listener(APIUrl);
            apiClient.CounterChanged += _apiClient_ListenerCounterChanged;
            activeClients.Add(apiClient);

            Thread apiThread = new Thread(() =>
            {
                apiClient.StartMonitoringAsync();
            });

            activeThreads.Add(apiThread);
            apiThread.Start();

            RegisterListener(apiClient);


        }


        private void RegisterListener(Listener listener)
        {
            ListViewItem listItem = new ListViewItem(listener.Name);
            listItem.SubItems.Add(listener.Target.ToString());
            listItem.SubItems.Add(listener.Counter.ToString());

            ListenersListView.Items.Add(listItem);

            listenerListViewMap[listener.Name] = listItem;
        }


        private void _apiClient_ListenerCounterChanged(object? sender, int counter)
        {
            if (sender is Listener listener)
            {
                string listenerName = listener.Name;


                UpdateListView(listenerName, counter);
            }
        }

        private void UpdateListView(string listenerName, int counter)
        {
            if (ListenersListView.InvokeRequired)
            {
                Action<string, int> updateListViewDelegate = UpdateListView;
                this.Invoke(updateListViewDelegate, new object[] { listenerName, counter });
            }
            else
            {
                if (listenerListViewMap.TryGetValue(listenerName, out ListViewItem listItem))
                {
                    listItem.SubItems[2].Text = counter.ToString();
                }
            }
        }


        private void StopButton_Click(object sender, EventArgs e)
        {

            Thread stopThread = new Thread(() =>
            {


                foreach (var apiClient in activeClients)
                {
                    apiClient.StopMonitoring();
                }

                activeClients.Clear();

                this.Invoke((MethodInvoker) delegate { 
                    ListenersListView.Items.Clear();
            
                });  

                listenerListViewMap.Clear();

                Active = false;

            });

            stopThread.Start();
                FlipButtonState(ref StartButton);
                FlipButtonState(ref StopButton);
                FlipButtonState(ref RegisterButton);
                FlipButtonState(ref UnregisterButton);
        }

        private void FlipButtonState(ref Button button)
        {
            button.Enabled = !button.Enabled;
        }

        private void RegisterButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(APIUrl)) { return; }


            Listener apiClient = new Listener(APIUrl);
            apiClient.CounterChanged += _apiClient_ListenerCounterChanged;
            activeClients.Add(apiClient);

            Thread apiThread = new Thread(() =>
            {
                apiClient.StartMonitoringAsync();
            });

            activeThreads.Add(apiThread);
            apiThread.Start();

            RegisterListener(apiClient);

        }

        private void UnregisterButton_Click(object sender, EventArgs e)
        {
            if (ListenersListView.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = ListenersListView.SelectedItems[0];

                string listenerName = selectedItem.Text;

                Listener apiClient = activeClients.FirstOrDefault(client => client.Name == listenerName);

                if (apiClient != null)
                {
                    Thread unregisterThread = new Thread(() =>
                    {
                        apiClient.StopMonitoring();

                        this.Invoke((MethodInvoker)delegate
                        {
                            ListenersListView.Items.Remove(selectedItem);
                        });

                        listenerListViewMap.Remove(listenerName);

                        activeClients.Remove(apiClient);
                    });

                    unregisterThread.Start();
                }
            }
        }



    }
}