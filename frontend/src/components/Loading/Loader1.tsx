"use client";

import Image from 'next/image';
import 'tailwindcss/tailwind.css';

const Loader1 = () => {
    return (
        <div className="fixed inset-0 flex items-center justify-center z-50 bg-black">
            <div className="max-w-xs mx-auto">
                <Image
                    src="/assets/gif/circle-pattern.gif"
                    alt="Loading"
                    width={100}
                    height={100}
                    className="img-fluid"
                    priority={true}
                    quality={100}
                    unoptimized={true}
                    />
            </div>
        </div>
    );
}

export default Loader1