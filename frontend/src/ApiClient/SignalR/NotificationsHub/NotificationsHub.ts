import Notifications from '../../../Notifications/Notifications';
import HubProxy from '../HubProxy';

const API_URL = process.env.REACT_APP_API_URL!;
const HUB_URL = `${API_URL}/signalr/notifications`;

const proxy = new HubProxy();

proxy.on('UserLoggedIn', (username: string) => {
  Notifications.showSuccessNotification(
    'User logged in!',
    `User ${username} just logged in!`
  );
});

async function connect(): Promise<void> {
  await proxy.connect(HUB_URL);
}

async function sendNotification(): Promise<void> {
  await proxy.send('SentSomething', 'hello');
}
export default { connect, sendNotification };
