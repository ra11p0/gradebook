import Notifications from '../../../Notifications/Notifications';
import HubProxy, { Subscriber } from '../HubProxy';

const API_URL = process.env.REACT_APP_API_URL;
const HUB_URL = `${API_URL}/signalr/notifications`;

export default class NotificationsHub {
    public static proxy: HubProxy = new HubProxy();
    private static _initialize = (() => {
        this.proxy.on('UserLoggedIn', (username) => {
            Notifications.showSuccessNotification("User logged in!", `User ${username} just logged in!`)
        })
    })();
    static connect() {
        this.proxy.connect(HUB_URL)
    }
    static sendNotification() {
        this.proxy.send('SentSomething', "hello");
    }
}
