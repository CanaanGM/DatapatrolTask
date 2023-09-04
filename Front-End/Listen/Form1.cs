using Listen.Data;

using System.Diagnostics.Metrics;

namespace Listen
{
    public partial class MainForm : Form
    {

        public bool Active { get; set; } = false;
        private List<APIClient> activeClients = new List<APIClient>();
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
            Active = true;

            APIUrl = BaseUrlTextBox.Text;


            APIClient apiClient = new APIClient(APIUrl);
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


        private void RegisterListener(APIClient listener)
        {
            ListViewItem listItem = new ListViewItem(listener.Name);
            listItem.SubItems.Add(listener.Target.ToString());
            listItem.SubItems.Add(listener.Counter.ToString());

            ListenersListView.Items.Add(listItem);

            listenerListViewMap[listener.Name] = listItem;
        }


        private void _apiClient_ListenerCounterChanged(object? sender, int counter)
        {
            if (sender is APIClient listener)
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
            foreach (var apiClient in activeClients)
            {
                apiClient.StopMonitoring();
            }

            activeClients.Clear();

            ListenersListView.Items.Clear();

            listenerListViewMap.Clear();

            FlipButtonState(ref StartButton);
            FlipButtonState(ref StopButton);
            Active = false;


        }

        private void FlipButtonState(ref Button button)
        {
            button.Enabled = !button.Enabled;
        }

        private void RegisterButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(APIUrl)) { return; }


            APIClient apiClient = new APIClient(APIUrl);
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

                APIClient apiClient = activeClients.FirstOrDefault(client => client.Name == listenerName);

                if (apiClient != null)
                {
                    apiClient.StopMonitoring();

                    ListenersListView.Items.Remove(selectedItem);

                    listenerListViewMap.Remove(listenerName);

                    activeClients.Remove(apiClient);
                }
            }
        }


    }
}