"use client"

import { useEffect } from "react"
import { motion, stagger, useAnimate } from "framer-motion"

export const TextGenerateEffect = ({
  words,
  className,
  as: Component = "div",
}: {
  words: string
  className?: string
  as?: keyof JSX.IntrinsicElements
}) => {
  const [scope, animate] = useAnimate()
  let wordsArray = words.split(" ")

  useEffect(() => {
    animate(
      "span",
      {
        opacity: 1,
      },
      {
        duration: 4,
        delay: stagger(0.2),
      }
    )
  }, [scope, animate])

  const renderWords = () => {
    return (
      <motion.div ref={scope}>
        {wordsArray.map((word, idx) => {
          return (
            <motion.span
              key={word + idx}
              initial={{ opacity: 0 }}
            >
              {word}{" "}
            </motion.span>
          )
        })}
      </motion.div>
    )
  }

  return <Component className={className}>{renderWords()}</Component>
}