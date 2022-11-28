import NotificationsHub from './NotificationsHub/NotificationsHub';

async function connectAllHubs(): Promise<void> {
  await NotificationsHub.connect();
}
export { connectAllHubs };
