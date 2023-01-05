import { defineConfig } from 'vite';
import react from '@vitejs/plugin-react';
import viteTsconfigPaths from 'vite-tsconfig-paths';
import svgrPlugin from 'vite-plugin-svgr';
import { configDefaults } from 'vitest/config';

// https://vitejs.dev/config/
export default defineConfig({
  build: {
    outDir: 'build',
    target: 'esnext',
    rollupOptions: {
      output: {
        manualChunks(id) {
          if (id.includes('i18n/locales')) {
            return 'i18n';
          }
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
    testTimeout: 10000,
    exclude: [...configDefaults.exclude],
  },
});
