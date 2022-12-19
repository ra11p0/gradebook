import { defineConfig } from 'vite';
import react from '@vitejs/plugin-react';
import viteTsconfigPaths from 'vite-tsconfig-paths';
import svgrPlugin from 'vite-plugin-svgr';

// https://vitejs.dev/config/
export default defineConfig({
  build: {
    outDir: 'build',
    target: 'esnext',
  },
  server: {
    port: 3005,
    host: '127.0.0.1',
  },
  plugins: [react(), viteTsconfigPaths(), svgrPlugin()],
  test: {
    globals: true,
    environment: 'jsdom',
  },
  define: {
    'process.env': {},
  },
});
