import { defineConfig } from 'vite';
import react from '@vitejs/plugin-react';
import viteTsconfigPaths from 'vite-tsconfig-paths';
import svgrPlugin from 'vite-plugin-svgr';

const maxChunks = 5;
let chunks = 0;

// https://vitejs.dev/config/
export default defineConfig({
  build: {
    outDir: 'build',
    target: 'esnext',
    rollupOptions: {
      output: {
        manualChunks(id) {
          chunks++;
          return `chunk_${chunks % maxChunks}`;
        },
      },
    },
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
