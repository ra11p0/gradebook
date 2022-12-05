import NotificationsHub from './NotificationsHub/NotificationsHub';

const allHubs = [NotificationsHub];

async function connectAllHubs(): Promise<void> {
  await allHubs.forEach(async (hub) => {
    await hub.connect();
  });
}

async function disconnectAllHubs(): Promise<void> {
  await allHubs.forEach(async (hub) => {
    await hub.disconnect();
  });
}

export { connectAllHubs, disconnectAllHubs };
