using Listen.Data;

using System.Diagnostics.Metrics;

namespace Listen
{
    public partial class MainForm : Form
    {

        private List<Listener> activeListeners = new List<Listener>();
        private List<Thread> activeThreads = new List<Thread>();
        private Dictionary<string, ListViewItem> listenerListViewMap = new Dictionary<string, ListViewItem>();

        private string? APIUrl;
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


            APIUrl = BaseUrlTextBox.Text;
            BaseUrlTextBox.Enabled = false;
            BaseUrlTextBox.BackColor = Color.LightGoldenrodYellow;

            Listener listener = new Listener(APIUrl);
            listener.CounterChanged += _listenerCounterChanged;
            activeListeners.Add(listener);

            Thread listenerThread = new Thread(() =>
            {
                listener?.StartMonitoringAsync();
            });

            activeThreads.Add(listenerThread);
            listenerThread.Start();

            RegisterListener(listener);
        }


        private void RegisterListener(Listener listener)
        {
            ListViewItem listItem = new ListViewItem(listener.Name);

            listItem.SubItems.Add(listener.Target.ToString());
            listItem.SubItems.Add(listener.Counter.ToString());

            ListenersListView.Items.Add(listItem);
            listenerListViewMap[listener.Name] = listItem;
        }


        private void _listenerCounterChanged(object? sender, int counter)
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
                if (listenerListViewMap.TryGetValue(listenerName, out ListViewItem? listItem))
                {
                    listItem.SubItems[2].Text = counter.ToString();
                }
            }
        }


        private void StopButton_Click(object sender, EventArgs e)
        {

            Thread stopThread = new Thread(() =>
            {
                foreach (var activeListener in activeListeners)
                {
                    activeListener.StopMonitoring();
                }
                activeListeners.Clear();

                this.Invoke((MethodInvoker)delegate
                {
                    ListenersListView.Items.Clear();

                });

                listenerListViewMap.Clear();

                BeginInvoke((MethodInvoker)delegate
                {
                    FlipButtonState(ref StartButton);
                    BaseUrlTextBox.BackColor = Color.White;
                    BaseUrlTextBox.Enabled = true;
                    stopLbl.Visible = false;
                });
            });

            stopLbl.Visible = true;
            FlipButtonState(ref StopButton);
            FlipButtonState(ref RegisterButton);
            FlipButtonState(ref UnregisterButton);
            stopThread.Start();
        }

        private void FlipButtonState(ref Button button)
        {
            button.Enabled = !button.Enabled;
        }

        private void RegisterButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(APIUrl))
                return;


            Listener listener = new Listener(APIUrl);
            listener.CounterChanged += _listenerCounterChanged;
            activeListeners.Add(listener);

            Thread apiThread = new Thread(() =>
            {
                listener?.StartMonitoringAsync();
            });

            activeThreads.Add(apiThread);
            apiThread.Start();

            RegisterListener(listener);

        }

        private void UnregisterButton_Click(object sender, EventArgs e)
        {
            if (ListenersListView.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = ListenersListView.SelectedItems[0];

                string listenerName = selectedItem.Text;

                Listener? listner = activeListeners?.FirstOrDefault(client => client.Name == listenerName);

                if (listner != null)
                {
                    Thread unregisterThread = new Thread(() =>
                    {
                        listner.StopMonitoring();

                        this.Invoke((MethodInvoker)delegate
                        {
                            ListenersListView.Items.Remove(selectedItem);
                        });

                        listenerListViewMap.Remove(listenerName);

                        activeListeners?.Remove(listner);
                    });

                    unregisterThread.Start();
                }
            }
        }



    }
}