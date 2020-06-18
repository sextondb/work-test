import { AppProps } from 'next/app'

function App({ Component, pageProps }: AppProps) {
    const content = <Component {...pageProps} />;
    return content
}

export default App
